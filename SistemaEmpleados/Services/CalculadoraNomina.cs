namespace SistemaEmpleados.Servicios
{
    using SistemaEmpleados.Models;

    /// <summary>
    /// Clase SEALED - no debe ser heredada
    /// Responsabilidad única: calcular nóminas
    /// </summary>
    public sealed class CalculadoraNomina
    {
        // Usar POLIMORFISMO en lugar de switches
        public decimal CalcularNominaTotal(List<Empleado> empleados)
        {
            decimal total = 0;

            foreach (var empleado in empleados)
            {
                // Polimorfismo: cada empleado sabe cómo calcularse
                total += empleado.CalcularSalario();
            }

            return total;
        }

        public Dictionary<string, decimal> CalcularNominaPorTipo(List<Empleado> empleados)
        {
            var nominaPorTipo = new Dictionary<string, decimal>
            {
                { "EmpleadoPermanente", 0m },
                { "Contratista", 0m },
                { "Pasante", 0m }
            };

            foreach (var empleado in empleados)
            {
                string tipo = empleado.GetType().Name;
                decimal salario = empleado.CalcularSalario();

                if (nominaPorTipo.ContainsKey(tipo))
                    nominaPorTipo[tipo] += salario;
            }

            return nominaPorTipo;
        }

        public ResumenNomina GenerarResumen(List<Empleado> empleados)
        {
            var resumen = new ResumenNomina
            {
                TotalEmpleados = empleados.Count,
                NominaTotal = CalcularNominaTotal(empleados),
                FechaGeneracion = DateTime.Now
            };

            // Calcular estadísticas
            if (empleados.Count > 0)
            {
                resumen.SalarioPromedio = resumen.NominaTotal / empleados.Count;
                resumen.SalarioMinimo = empleados.Min(e => e.CalcularSalario());
                resumen.SalarioMaximo = empleados.Max(e => e.CalcularSalario());
            }

            resumen.NominaPorTipo = CalcularNominaPorTipo(empleados);

            return resumen;
        }
    }

    /// <summary>
    /// Clase SEALED para almacenar resultados
    /// DTO (Data Transfer Object) - no necesita herencia
    /// </summary>
    public sealed class ResumenNomina
    {
        public int TotalEmpleados { get; set; }
        public decimal NominaTotal { get; set; }
        public decimal SalarioPromedio { get; set; }
        public decimal SalarioMinimo { get; set; }
        public decimal SalarioMaximo { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public Dictionary<string, decimal> NominaPorTipo { get; set; }

        public void Imprimir()
        {
            Console.WriteLine("\n╔══════════════════════════════════════════╗");
            Console.WriteLine("║       RESUMEN DE NÓMINA                  ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine($"Fecha: {FechaGeneracion:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Total Empleados: {TotalEmpleados}");
            Console.WriteLine($"Nómina Total: ${NominaTotal:N2}");
            Console.WriteLine($"Salario Promedio: ${SalarioPromedio:N2}");
            Console.WriteLine($"Salario Mínimo: ${SalarioMinimo:N2}");
            Console.WriteLine($"Salario Máximo: ${SalarioMaximo:N2}");

            Console.WriteLine("\n--- Nómina por Tipo ---");
            foreach (var kvp in NominaPorTipo)
            {
                Console.WriteLine($"{kvp.Key}: ${kvp.Value:N2}");
            }
        }
    }
}