using SistemaEmpleados.Models;
using SistemaEmpleados.Servicios;

namespace SistemaEmpleados
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║     SISTEMA DE GESTIÓN DE EMPLEADOS - DÍA 4          ║");
            Console.WriteLine("║     Refactorización y Buenas Prácticas               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            // Crear el gestor principal (sealed class que coordina)
            var gestor = new GestorEmpleados();

            // Crear empleados de prueba
            CrearEmpleadosDePrueba(gestor);

            // Menú interactivo
            bool continuar = true;
            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        gestor.MostrarEstadisticas();
                        break;

                    case "2":
                        gestor.ProcesarNomina();
                        break;

                    case "3":
                        gestor.MostrarReportePorTipo();
                        break;

                    case "4":
                        gestor.ImprimirReportesIndividuales();
                        break;

                    case "5":
                        gestor.GenerarReporteCompleto();
                        break;

                    case "6":
                        Console.Write("\nFormato (json/csv/xml): ");
                        string formato = Console.ReadLine();
                        gestor.ExportarEmpleados(formato);
                        break;

                    case "7":
                        gestor.ValidarTodosLosEmpleados();
                        break;

                    case "8":
                        AgregarNuevoEmpleado(gestor);
                        break;

                    case "9":
                        BuscarEmpleado(gestor);
                        break;

                    case "0":
                        continuar = false;
                        Console.WriteLine("\n¡Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("\n✗ Opción no válida");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    MENÚ PRINCIPAL                     ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.WriteLine("1. Mostrar Estadísticas");
            Console.WriteLine("2. Procesar Nómina");
            Console.WriteLine("3. Reporte por Tipo");
            Console.WriteLine("4. Imprimir Reportes Individuales");
            Console.WriteLine("5. Generar Reporte Completo (archivo)");
            Console.WriteLine("6. Exportar Empleados");
            Console.WriteLine("7. Validar Empleados");
            Console.WriteLine("8. Agregar Nuevo Empleado");
            Console.WriteLine("9. Buscar Empleado");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        static void CrearEmpleadosDePrueba(GestorEmpleados gestor)
        {
            Console.WriteLine("Cargando empleados de prueba...\n");

            // Empleados permanentes
            gestor.AgregarEmpleado(new EmpleadoPermanente
            {
                Nombre = "Ana Martínez",
                Id = 1001,
                SalarioBase = 60000m,
                FechaIngreso = new DateTime(2020, 1, 15)
            });

            gestor.AgregarEmpleado(new EmpleadoPermanente
            {
                Nombre = "Carlos Ruiz",
                Id = 1002,
                SalarioBase = 75000m,
                FechaIngreso = new DateTime(2018, 6, 10)
            });

            // Contratistas
            gestor.AgregarEmpleado(new Contratista
            {
                Nombre = "María García",
                Id = 2001,
                TarifaPorHora = 350m,
                HorasTrabajadas = 160,
                FechaIngreso = new DateTime(2024, 3, 1)
            });

            gestor.AgregarEmpleado(new Contratista
            {
                Nombre = "Luis Hernández",
                Id = 2002,
                TarifaPorHora = 280m,
                HorasTrabajadas = 120,
                FechaIngreso = new DateTime(2024, 7, 15)
            });

            // Pasantes
            gestor.AgregarEmpleado(new Pasante
            {
                Nombre = "Sofia López",
                Id = 3001,
                EstipendioMensual = 8500m,
                Universidad = "UNAM",
                Carrera = "Ingeniería en Computación",
                FechaIngreso = new DateTime(2024, 9, 1)
            });

            gestor.AgregarEmpleado(new Pasante
            {
                Nombre = "Diego Torres",
                Id = 3002,
                EstipendioMensual = 9000m,
                Universidad = "IPN",
                Carrera = "Ingeniería en Sistemas",
                FechaIngreso = new DateTime(2024, 10, 1)
            });

            Console.WriteLine("\n✓ Empleados cargados exitosamente\n");
        }

        static void AgregarNuevoEmpleado(GestorEmpleados gestor)
        {
            Console.WriteLine("\n=== AGREGAR NUEVO EMPLEADO ===");
            Console.WriteLine("Tipo de empleado:");
            Console.WriteLine("1. Permanente");
            Console.WriteLine("2. Contratista");
            Console.WriteLine("3. Pasante");
            Console.Write("Seleccione: ");

            string tipo = Console.ReadLine();

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            switch (tipo)
            {
                case "1":
                    Console.Write("Salario Base: ");
                    decimal salario = decimal.Parse(Console.ReadLine());

                    gestor.AgregarEmpleado(new EmpleadoPermanente
                    {
                        Nombre = nombre,
                        Id = id,
                        SalarioBase = salario,
                        FechaIngreso = DateTime.Now
                    });
                    break;

                case "2":
                    Console.Write("Tarifa por Hora: ");
                    decimal tarifa = decimal.Parse(Console.ReadLine());

                    Console.Write("Horas Trabajadas: ");
                    int horas = int.Parse(Console.ReadLine());

                    gestor.AgregarEmpleado(new Contratista
                    {
                        Nombre = nombre,
                        Id = id,
                        TarifaPorHora = tarifa,
                        HorasTrabajadas = horas,
                        FechaIngreso = DateTime.Now
                    });
                    break;

                case "3":
                    Console.Write("Estipendio Mensual: ");
                    decimal estipendio = decimal.Parse(Console.ReadLine());

                    Console.Write("Universidad: ");
                    string universidad = Console.ReadLine();

                    gestor.AgregarEmpleado(new Pasante
                    {
                        Nombre = nombre,
                        Id = id,
                        EstipendioMensual = estipendio,
                        Universidad = universidad,
                        FechaIngreso = DateTime.Now
                    });
                    break;

                default:
                    Console.WriteLine("Tipo no válido");
                    break;
            }
        }

        static void BuscarEmpleado(GestorEmpleados gestor)
        {
            Console.WriteLine("\n=== BUSCAR EMPLEADO ===");
            Console.WriteLine("1. Por ID");
            Console.WriteLine("2. Por Nombre");
            Console.Write("Seleccione: ");

            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("ID: ");
                int id = int.Parse(Console.ReadLine());

                var empleado = gestor.BuscarPorID(id);
                if (empleado != null)
                {
                    Console.WriteLine();
                    empleado.MostrarInformacion();
                }
                else
                {
                    Console.WriteLine($"\n✗ No se encontró empleado con ID {id}");
                }
            }
            else if (opcion == "2")
            {
                Console.Write("Nombre (o parte del nombre): ");
                string nombre = Console.ReadLine();

                var empleados = gestor.BuscarPorNombre(nombre);
                if (empleados.Count > 0)
                {
                    Console.WriteLine($"\n✓ Se encontraron {empleados.Count} empleado(s):\n");
                    foreach (var emp in empleados)
                    {
                        Console.WriteLine($"ID: {emp.Id} - {emp.Nombre} ({emp.GetType().Name})");
                    }
                }
                else
                {
                    Console.WriteLine($"\n✗ No se encontraron empleados con '{nombre}'");
                }
            }
        }
    }
}