namespace SistemaEmpleados.Servicios
{
    using SistemaEmpleados.Models;
    using SistemaEmpleados.Services;

    /// <summary>
    /// Gestor principal que COORDINA servicios especializados
    /// No hace todo - DELEGA a clases especializadas (SRP)
    /// </summary>
    public sealed class GestorEmpleados
    {
        // Composición: el gestor TIENE servicios, no los hereda
        private readonly CalculadoraNomina _calculadora;
        private readonly GeneradorReportes _generadorReportes;
        private readonly ServicioValidacion _validador;
        private readonly ServicioExportacion _exportador;

        // Lista interna de empleados
        private readonly List<Empleado> _empleados;

        public GestorEmpleados()
        {
            _calculadora = new CalculadoraNomina();
            _generadorReportes = new GeneradorReportes();
            _validador = new ServicioValidacion();
            _exportador = new ServicioExportacion();
            _empleados = new List<Empleado>();
        }

        // ========== GESTIÓN DE EMPLEADOS ==========

        public void AgregarEmpleado(Empleado empleado)
        {
            // Validar antes de agregar
            if (empleado is IValidable validable)
            {
                if (!validable.EsValido())
                {
                    Console.WriteLine($"\n✗ No se puede agregar {empleado.Nombre}:");
                    _validador.MostrarErroresValidacion(validable);
                    return;
                }
            }

            // Verificar que no exista el ID
            if (_empleados.Any(e => e.Id == empleado.Id))
            {
                Console.WriteLine($"✗ Ya existe un empleado con ID {empleado.Id}");
                return;
            }

            _empleados.Add(empleado);
            Console.WriteLine($"✓ Empleado agregado: {empleado.Nombre} (ID: {empleado.Id})");
        }

        public Empleado BuscarPorID(int id)
        {
            return _empleados.FirstOrDefault(e => e.Id == id);
        }

        public List<Empleado> BuscarPorNombre(string nombre)
        {
            return _empleados
                .Where(e => e.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void EliminarEmpleado(int id)
        {
            var empleado = BuscarPorID(id);
            if (empleado != null)
            {
                _empleados.Remove(empleado);
                Console.WriteLine($"✓ Empleado eliminado: {empleado.Nombre}");
            }
            else
            {
                Console.WriteLine($"✗ No se encontró empleado con ID {id}");
            }
        }

        public List<Empleado> ObtenerTodos()
        {
            return new List<Empleado>(_empleados); // Retornar copia
        }

        // ========== OPERACIONES DE NÓMINA ==========

        public void ProcesarNomina()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║              PROCESAMIENTO DE NÓMINA                  ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            if (_empleados.Count == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
                return;
            }

            // Delegar al calculador especializado
            var resumen = _calculadora.GenerarResumen(_empleados);
            resumen.Imprimir();
        }

        public decimal ObtenerNominaTotal()
        {
            return _calculadora.CalcularNominaTotal(_empleados);
        }

        // ========== REPORTES ==========

        public void GenerarReporteCompleto(string nombreArchivo = "reporte_completo.txt")
        {
            _generadorReportes.GenerarReporteDetallado(_empleados, nombreArchivo);
        }

        public void MostrarReportePorTipo()
        {
            _generadorReportes.GenerarReportePorTipo(_empleados);
        }

        public void ImprimirReportesIndividuales()
        {
            // Filtrar solo los que implementan IImprimible
            var imprimibles = _empleados.OfType<IImprimible>().ToList();
            _generadorReportes.ImprimirReportesIndividuales(imprimibles);
        }

        // ========== EXPORTACIÓN ==========

        public void ExportarEmpleados(string formato)
        {
            // Filtrar solo los que implementan IExportable
            var exportables = _empleados.OfType<IExportable>().ToList();

            if (exportables.Count == 0)
            {
                Console.WriteLine("No hay empleados exportables.");
                return;
            }

            switch (formato.ToLower())
            {
                case "json":
                    _exportador.ExportarEmpleadosAJSON(exportables, "empleados.json");
                    break;
                case "csv":
                    _exportador.ExportarEmpleadosACSV(exportables, "empleados.csv");
                    break;
                case "xml":
                    _exportador.ExportarEmpleadosAXML(exportables, "empleados.xml");
                    break;
                default:
                    Console.WriteLine($"Formato no soportado: {formato}");
                    break;
            }
        }

        // ========== VALIDACIÓN ==========

        public void ValidarTodosLosEmpleados()
        {
            var validables = _empleados.OfType<IValidable>().ToList();
            _validador.ValidarLista(validables);
        }

        // ========== ESTADÍSTICAS ==========

        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║              ESTADÍSTICAS DEL SISTEMA                 ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            Console.WriteLine($"Total de Empleados: {_empleados.Count}");

            // Contar por tipo
            var permanentes = _empleados.OfType<EmpleadoPermanente>().Count();
            var contratistas = _empleados.OfType<Contratista>().Count();
            var pasantes = _empleados.OfType<Pasante>().Count();

            Console.WriteLine($"  • Permanentes: {permanentes}");
            Console.WriteLine($"  • Contratistas: {contratistas}");
            Console.WriteLine($"  • Pasantes: {pasantes}");

            if (_empleados.Count > 0)
            {
                var empleadoMasAntiguo = _empleados.OrderBy(e => e.FechaIngreso).First();
                var empleadoMasReciente = _empleados.OrderByDescending(e => e.FechaIngreso).First();
                var salarioMasAlto = _empleados.Max(e => e.CalcularSalario());
                var salarioMasBajo = _empleados.Min(e => e.CalcularSalario());

                Console.WriteLine($"\nEmpleado más antiguo: {empleadoMasAntiguo.Nombre} ({empleadoMasAntiguo.FechaIngreso:dd/MM/yyyy})");
                Console.WriteLine($"Empleado más reciente: {empleadoMasReciente.Nombre} ({empleadoMasReciente.FechaIngreso:dd/MM/yyyy})");
                Console.WriteLine($"Salario más alto: ${salarioMasAlto:N2}");
                Console.WriteLine($"Salario más bajo: ${salarioMasBajo:N2}");
            }
        }
    }
}