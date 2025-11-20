namespace SistemaEmpleados.Servicios
{
    using SistemaEmpleados.Models;

    public class ServicioValidacion
    {
        // Trabajamos con IValidable, no con clases específicas
        public bool ValidarEmpleado(IValidable empleado)
        {
            return empleado.EsValido();
        }

        public void MostrarErroresValidacion(IValidable empleado)
        {
            var errores = empleado.ObtenerErrores();

            if (errores.Count == 0)
            {
                Console.WriteLine("✓ Validación exitosa");
                return;
            }

            Console.WriteLine($"✗ Se encontraron {errores.Count} errores:");
            foreach (var error in errores)
            {
                Console.WriteLine($"  - {error}");
            }
        }

        public void ValidarLista(List<IValidable> empleados)
        {
            Console.WriteLine("\n=== VALIDACIÓN DE EMPLEADOS ===");

            int validos = 0;
            int invalidos = 0;

            foreach (var empleado in empleados)
            {
                // Obtener nombre si es un Empleado
                string nombre = "Desconocido";
                if (empleado is Empleado emp)
                    nombre = emp.Nombre;

                Console.WriteLine($"\nValidando: {nombre}");

                if (empleado.EsValido())
                {
                    Console.WriteLine("  ✓ Válido");
                    validos++;
                }
                else
                {
                    Console.WriteLine("  ✗ Inválido");
                    MostrarErroresValidacion(empleado);
                    invalidos++;
                }
            }

            Console.WriteLine($"\n--- RESUMEN ---");
            Console.WriteLine($"Válidos: {validos}");
            Console.WriteLine($"Inválidos: {invalidos}");
        }
    }
}