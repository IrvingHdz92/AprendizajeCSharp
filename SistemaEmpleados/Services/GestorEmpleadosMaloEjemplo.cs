namespace SistemaEmpleados.Servicios
{
    using SistemaEmpleados.Models;

    /// <summary>
    /// EJEMPLO DE MAL DISEÑO - NO USAR EN PRODUCCIÓN
    /// Violaciones: God Class, Switch en tipos, Sin polimorfismo
    /// </summary>
    public class GestorEmpleadosMaloEjemplo
    {
        // MAL: God Class - esta clase hace DEMASIADO

        public void ProcesarNomina(List<Empleado> empleados)
        {
            decimal total = 0;

            foreach (var empleado in empleados)
            {
                // MAL: Switch en tipos - violación de polimorfismo
                if (empleado is EmpleadoPermanente permanente)
                {
                    // Lógica específica para permanente
                    decimal salario = permanente.SalarioBase;
                    int años = permanente.ObtenerAntiguedad();
                    decimal bono = salario * (años * 0.02m);
                    total += salario + bono;

                    // Lógica de impresión mezclada con lógica de negocio
                    Console.WriteLine($"Permanente: {permanente.Nombre} - ${salario + bono}");
                }
                else if (empleado is Contratista contratista)
                {
                    // Duplicación de lógica
                    decimal salario = contratista.TarifaPorHora * contratista.HorasTrabajadas;
                    total += salario;
                    Console.WriteLine($"Contratista: {contratista.Nombre} - ${salario}");
                }
                else if (empleado is Pasante pasante)
                {
                    decimal salario = pasante.EstipendioMensual;
                    total += salario;
                    Console.WriteLine($"Pasante: {pasante.Nombre} - ${salario}");
                }
                // Si agregamos un nuevo tipo, hay que modificar este método
            }

            Console.WriteLine($"Total: ${total}");
        }

        public void GenerarReportes(List<Empleado> empleados)
        {
            // MAL: Más switches en tipos
            foreach (var empleado in empleados)
            {
                if (empleado is EmpleadoPermanente)
                {
                    // Genera reporte de permanente
                }
                else if (empleado is Contratista)
                {
                    // Genera reporte de contratista
                }
                // ...
            }
        }

        public void ValidarEmpleados(List<Empleado> empleados)
        {
            // MAL: Otra vez switches en tipos
            // ...
        }

        // ... más métodos que hacen demasiadas cosas ...
    }
}