using SistemaEmpleados.Generics;
using SistemaEmpleados.Models;
using SistemaEmpleados.Servicios;

namespace SistemaEmpleados
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║         SEMANA 2 - DÍA 1: GENÉRICOS                   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            // ========== PARTE 1: Contenedor Genérico ==========
            DemostrarContenedorGenerico();

            // ========== PARTE 2: Par Ordenado ==========
            DemostrarParOrdenado();

            // ========== PARTE 3: Métodos Genéricos ==========
            DemostrarMetodosGenericos();

            // ========== PARTE 4: Colecciones Genéricas ==========
            Console.WriteLine("\n\n" + new string('=', 60));
            Console.WriteLine("COLECCIONES GENÉRICAS DE .NET");
            Console.WriteLine(new string('=', 60));

            DemostracionColecciones.DemostrarList();
            DemostracionColecciones.DemostrarDictionary();
            DemostracionColecciones.DemostrarHashSet();
            DemostracionColecciones.DemostrarQueue();
            DemostracionColecciones.DemostrarStack();
            DemostracionColecciones.DemostrarLinkedList();
            DemostracionColecciones.DemostrarSortedDictionary();

            Console.WriteLine("\n\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void DemostrarContenedorGenerico()
        {
            Console.WriteLine("═══ PARTE 1: Contenedor Genérico ═══\n");

            // Contenedor de enteros
            var contenedorInt = new Contenedor<int>(42);
            Console.WriteLine("--- Contenedor<int> ---");
            contenedorInt.MostrarInformacion();

            // Contenedor de strings
            var contenedorString = new Contenedor<string>("Hola Genéricos");
            Console.WriteLine("\n--- Contenedor<string> ---");
            contenedorString.MostrarInformacion();

            // Contenedor de empleados
            var empleado = new EmpleadoPermanente
            {
                Nombre = "Juan Pérez",
                Id = 1001,
                SalarioBase = 50000m
            };

            var contenedorEmpleado = new Contenedor<EmpleadoPermanente>(empleado);
            Console.WriteLine("\n--- Contenedor<EmpleadoPermanente> ---");
            contenedorEmpleado.MostrarInformacion();
            Console.WriteLine($"Empleado almacenado: {contenedorEmpleado.Valor.Nombre}");
        }

        static void DemostrarParOrdenado()
        {
            Console.WriteLine("\n\n═══ PARTE 2: Par Ordenado (Múltiples Tipos) ═══\n");

            // Par de int y string
            var par1 = new ParOrdenado<int, string>(1001, "Ana Martínez");
            Console.WriteLine($"Par 1: {par1}");

            // Par de string y decimal
            var par2 = new ParOrdenado<string, decimal>("Salario", 75000.50m);
            Console.WriteLine($"Par 2: {par2}");

            // Intercambiar
            var par2Intercambiado = par2.Intercambiar();
            Console.WriteLine($"Par 2 Intercambiado: {par2Intercambiado}");

            // Par de tipos complejos
            var empleado1 = new EmpleadoPermanente { Nombre = "Carlos", Id = 1 };
            var empleado2 = new Contratista { Nombre = "María", Id = 2 };
            var parEmpleados = new ParOrdenado<EmpleadoPermanente, Contratista>(empleado1, empleado2);
            Console.WriteLine($"\nPar de empleados: ({parEmpleados.Primero.Nombre}, {parEmpleados.Segundo.Nombre})");
        }

        static void DemostrarMetodosGenericos()
        {
            Console.WriteLine("\n\n═══ PARTE 3: Métodos Genéricos ═══\n");

            // Intercambiar valores
            Console.WriteLine("--- Intercambiar ---");
            int a = 10, b = 20;
            Console.WriteLine($"Antes: a={a}, b={b}");
            UtilidadesGenericas.Intercambiar(ref a, ref b);
            Console.WriteLine($"Después: a={a}, b={b}");

            string x = "Hola", y = "Mundo";
            Console.WriteLine($"\nAntes: x={x}, y={y}");
            UtilidadesGenericas.Intercambiar(ref x, ref y);
            Console.WriteLine($"Después: x={x}, y={y}");

            // Imprimir arrays
            Console.WriteLine("\n--- Imprimir Arrays ---");
            int[] numeros = { 1, 2, 3, 4, 5 };
            string[] palabras = { "Hola", "Mundo", "Genéricos" };

            UtilidadesGenericas.ImprimirArray(numeros, "Array de Enteros");
            UtilidadesGenericas.ImprimirArray(palabras, "Array de Strings");

            // Buscar en array
            Console.WriteLine("\n--- Buscar Primero ---");
            int primerPar = UtilidadesGenericas.BuscarPrimero(numeros, n => n % 2 == 0);
            Console.WriteLine($"Primer número par: {primerPar}");

            string palabraLarga = UtilidadesGenericas.BuscarPrimero(palabras, p => p.Length > 5);
            Console.WriteLine($"Primera palabra larga: {palabraLarga}");

            // Contar elementos
            Console.WriteLine("\n--- Contar ---");
            int cantidadPares = UtilidadesGenericas.Contar(numeros, n => n % 2 == 0);
            Console.WriteLine($"Cantidad de números pares: {cantidadPares}");

            // Convertir tipos
            Console.WriteLine("\n--- Convertir ---");
            string[] numerosComoString = UtilidadesGenericas.Convertir(
                numeros,
                n => n.ToString());
            UtilidadesGenericas.ImprimirArray(numerosComoString, "Números convertidos a String");

            // Información de tipos
            Console.WriteLine("\n--- Información de Tipos ---");
            UtilidadesGenericas.MostrarInformacionTipo<int>();
            UtilidadesGenericas.MostrarInformacionTipo<string>();
            UtilidadesGenericas.MostrarInformacionTipo<EmpleadoPermanente>();

            // Comparar valores
            Console.WriteLine("\n--- Comparar Valores ---");
            bool iguales1 = UtilidadesGenericas.SonIguales(10, 10);
            bool iguales2 = UtilidadesGenericas.SonIguales("Hola", "Hola");
            bool iguales3 = UtilidadesGenericas.SonIguales(10, 20);

            Console.WriteLine($"10 == 10: {iguales1}");
            Console.WriteLine($"\"Hola\" == \"Hola\": {iguales2}");
            Console.WriteLine($"10 == 20: {iguales3}");

            // Repetir elementos
            Console.WriteLine("\n--- Repetir ---");
            var estrellas = UtilidadesGenericas.Repetir("★", 5);
            Console.WriteLine($"Estrellas: {string.Join(" ", estrellas)}");

            var ceros = UtilidadesGenericas.Repetir(0, 10);
            Console.WriteLine($"Ceros: [{string.Join(", ", ceros)}]");
        }
    }
}