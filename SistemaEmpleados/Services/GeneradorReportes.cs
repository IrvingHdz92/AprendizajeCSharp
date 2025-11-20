namespace SistemaEmpleados.Servicios
{
    using SistemaEmpleados.Models;
    using System.Text;

    /// <summary>
    /// Clase sealed para generar reportes
    /// Usa interfaces (IImprimible) en lugar de switches
    /// </summary>
    public sealed class GeneradorReportes
    {
        public void GenerarReporteDetallado(List<Empleado> empleados, string nombreArchivo)
        {
            Console.WriteLine($"\n=== Generando Reporte Detallado ===");

            var contenido = new StringBuilder();
            contenido.AppendLine("╔═══════════════════════════════════════════════════════╗");
            contenido.AppendLine("║          REPORTE DETALLADO DE EMPLEADOS               ║");
            contenido.AppendLine("╚═══════════════════════════════════════════════════════╝");
            contenido.AppendLine();
            contenido.AppendLine($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
            contenido.AppendLine($"Total de Empleados: {empleados.Count}");
            contenido.AppendLine();

            // Usar polimorfismo - no necesitamos saber el tipo específico
            foreach (var empleado in empleados)
            {
                contenido.AppendLine(new string('-', 55));
                contenido.AppendLine($"ID: {empleado.Id}");
                contenido.AppendLine($"Nombre: {empleado.Nombre}");
                contenido.AppendLine($"Tipo: {empleado.GetType().Name}");
                contenido.AppendLine($"Fecha Ingreso: {empleado.FechaIngreso:dd/MM/yyyy}");
                contenido.AppendLine($"Años Servicio: {empleado.ObtenerAntiguedad()}");
                contenido.AppendLine($"Salario: ${empleado.CalcularSalario():N2}");
                contenido.AppendLine($"Beneficios: {empleado.ObtenerBeneficios()}");
                contenido.AppendLine();
            }

            // Guardar archivo
            string ruta = Path.Combine(Environment.CurrentDirectory, nombreArchivo);
            File.WriteAllText(ruta, contenido.ToString());

            Console.WriteLine($"✓ Reporte guardado: {ruta}");
        }

        public void GenerarReportePorTipo(List<Empleado> empleados)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║          REPORTE POR TIPO DE EMPLEADO                 ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            // Agrupar por tipo usando LINQ
            var gruposPorTipo = empleados.GroupBy(e => e.GetType().Name);

            foreach (var grupo in gruposPorTipo)
            {
                Console.WriteLine($"\n--- {grupo.Key} ---");
                Console.WriteLine($"Cantidad: {grupo.Count()}");
                Console.WriteLine($"Nómina Total: ${grupo.Sum(e => e.CalcularSalario()):N2}");
                Console.WriteLine($"Salario Promedio: ${grupo.Average(e => e.CalcularSalario()):N2}");

                Console.WriteLine("\nEmpleados:");
                foreach (var empleado in grupo)
                {
                    Console.WriteLine($"  • {empleado.Nombre} - ${empleado.CalcularSalario():N2}");
                }
            }
        }

        public void ImprimirReportesIndividuales(List<IImprimible> empleados)
        {
            Console.WriteLine("\n=== REPORTES INDIVIDUALES ===");

            // Usar interface en lugar de casting
            foreach (var empleado in empleados)
            {
                if (empleado.PuedeImprimir)
                {
                    empleado.ImprimirEnConsola();
                    Console.WriteLine();
                }
            }
        }
    }
}