using SistemaEmpleados.Models;

namespace SistemaEmpleados.Generics
{    
    /// <summary>
    /// Demostración de las colecciones genéricas más comunes de .NET
    /// </summary>
    public static class DemostracionColecciones
    {
        public static void DemostrarList()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║              List<T> - Lista Dinámica                 ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

            // List<T> es la colección genérica más usada
            List<int> numeros = new List<int>();

            // Agregar elementos
            numeros.Add(10);
            numeros.Add(20);
            numeros.Add(30);
            numeros.AddRange(new[] { 40, 50 }); // Agregar varios

            Console.WriteLine($"Elementos en lista: {numeros.Count}");
            Console.WriteLine($"Capacidad actual: {numeros.Capacity}");

            // Acceso por índice
            Console.WriteLine($"Primer elemento: {numeros[0]}");
            Console.WriteLine($"Último elemento: {numeros[^1]}"); // C# 8.0+

            // Buscar
            bool contiene25 = numeros.Contains(25);
            int indice30 = numeros.IndexOf(30);
            Console.WriteLine($"¿Contiene 25?: {contiene25}");
            Console.WriteLine($"Índice de 30: {indice30}");

            // Iterar
            Console.Write("Elementos: ");
            foreach (int num in numeros)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            // Métodos LINQ
            int suma = numeros.Sum();
            double promedio = numeros.Average();
            int maximo = numeros.Max();

            Console.WriteLine($"Suma: {suma}, Promedio: {promedio:F2}, Máximo: {maximo}");
        }

        public static void DemostrarDictionary()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║        Dictionary<TKey, TValue> - Mapa/Diccionario   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

            // Dictionary: colección de pares clave-valor
            Dictionary<int, string> empleadosPorID = new Dictionary<int, string>();

            // Agregar elementos
            empleadosPorID.Add(1001, "Ana Martínez");
            empleadosPorID.Add(1002, "Carlos Ruiz");
            empleadosPorID[1003] = "María García"; // Otra forma de agregar

            Console.WriteLine($"Total de empleados: {empleadosPorID.Count}");

            // Acceso por clave
            string nombre = empleadosPorID[1001];
            Console.WriteLine($"Empleado 1001: {nombre}");

            // Verificar si existe una clave
            if (empleadosPorID.ContainsKey(1002))
            {
                Console.WriteLine($"Empleado 1002: {empleadosPorID[1002]}");
            }

            // TryGetValue: forma segura de obtener valores
            if (empleadosPorID.TryGetValue(9999, out string nombreNoExiste))
            {
                Console.WriteLine($"Encontrado: {nombreNoExiste}");
            }
            else
            {
                Console.WriteLine("ID 9999 no existe");
            }

            // Iterar sobre el diccionario
            Console.WriteLine("\nTodos los empleados:");
            foreach (KeyValuePair<int, string> kvp in empleadosPorID)
            {
                Console.WriteLine($"  ID {kvp.Key}: {kvp.Value}");
            }

            // Iterar solo claves o valores
            Console.WriteLine("\nSolo IDs:");
            foreach (int id in empleadosPorID.Keys)
            {
                Console.Write($"{id} ");
            }
            Console.WriteLine();
        }

        public static void DemostrarHashSet()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║         HashSet<T> - Conjunto Sin Duplicados          ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

            // HashSet: colección sin elementos duplicados
            HashSet<string> habilidades = new HashSet<string>();

            // Agregar elementos
            habilidades.Add("C#");
            habilidades.Add("SQL");
            habilidades.Add("JavaScript");
            habilidades.Add("C#"); // Duplicado - se ignora

            Console.WriteLine($"Habilidades únicas: {habilidades.Count}"); // 3, no 4

            // Verificar si contiene
            bool sabeSQL = habilidades.Contains("SQL");
            Console.WriteLine($"¿Sabe SQL?: {sabeSQL}");

            // Operaciones de conjuntos
            HashSet<string> habilidadesRequeridas = new HashSet<string>
            {
                "C#", "SQL", "Python"
            };

            // Intersección (habilidades que tiene Y son requeridas)
            var enComun = new HashSet<string>(habilidades);
            enComun.IntersectWith(habilidadesRequeridas);
            Console.WriteLine($"\nHabilidades en común: {string.Join(", ", enComun)}");

            // Unión (todas las habilidades)
            var todas = new HashSet<string>(habilidades);
            todas.UnionWith(habilidadesRequeridas);
            Console.WriteLine($"Todas las habilidades: {string.Join(", ", todas)}");

            // Diferencia (habilidades que tiene pero NO son requeridas)
            var extras = new HashSet<string>(habilidades);
            extras.ExceptWith(habilidadesRequeridas);
            Console.WriteLine($"Habilidades extras: {string.Join(", ", extras)}");
        }

        public static void DemostrarQueue()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║           Queue<T> - Cola FIFO                        ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

            // Queue: First In, First Out (primero en entrar, primero en salir)
            Queue<string> colaDeTareas = new Queue<string>();

            // Enqueue: agregar al final
            colaDeTareas.Enqueue("Procesar nómina");
            colaDeTareas.Enqueue("Generar reportes");
            colaDeTareas.Enqueue("Enviar emails");

            Console.WriteLine($"Tareas en cola: {colaDeTareas.Count}");

            // Peek: ver el primero sin removerlo
            string siguiente = colaDeTareas.Peek();
            Console.WriteLine($"Siguiente tarea: {siguiente}");
            Console.WriteLine($"Tareas en cola: {colaDeTareas.Count}"); // Sigue siendo 3

            // Dequeue: remover y obtener el primero
            Console.WriteLine("\nProcesando tareas:");
            while (colaDeTareas.Count > 0)
            {
                string tarea = colaDeTareas.Dequeue();
                Console.WriteLine($"  ✓ {tarea}");
            }

            Console.WriteLine($"Tareas restantes: {colaDeTareas.Count}");
        }

        public static void DemostrarStack()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║           Stack<T> - Pila LIFO                        ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

            // Stack: Last In, First Out (último en entrar, primero en salir)
            Stack<string> historialNavegacion = new Stack<string>();

            // Push: agregar al tope
            historialNavegacion.Push("Inicio");
            historialNavegacion.Push("Productos");
            historialNavegacion.Push("Detalle Producto");
            historialNavegacion.Push("Carrito");

            Console.WriteLine($"Páginas en historial: {historialNavegacion.Count}");

            // Peek: ver el tope sin removerlo
            string paginaActual = historialNavegacion.Peek();
            Console.WriteLine($"Página actual: {paginaActual}");

            // Pop: remover y obtener del tope
            Console.WriteLine("\nNavegando hacia atrás:");
            while (historialNavegacion.Count > 0)
            {
                string pagina = historialNavegacion.Pop();
                Console.WriteLine($"  ← {pagina}");
            }
        }

        public static void DemostrarLinkedList()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║       LinkedList<T> - Lista Doblemente Enlazada       ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

            // LinkedList: lista donde cada nodo apunta al siguiente y anterior
            // Eficiente para insertar/eliminar en medio
            LinkedList<string> playlist = new LinkedList<string>();

            // Agregar al final
            playlist.AddLast("Canción 1");
            playlist.AddLast("Canción 2");
            playlist.AddLast("Canción 3");

            // Agregar al inicio
            playlist.AddFirst("Intro");

            // Agregar después de un nodo específico
            LinkedListNode<string> nodo = playlist.Find("Canción 2");
            if (nodo != null)
            {
                playlist.AddAfter(nodo, "Canción 2.5");
            }

            Console.WriteLine("Playlist:");
            foreach (string cancion in playlist)
            {
                Console.WriteLine($"  ♪ {cancion}");
            }

            Console.WriteLine($"\nTotal de canciones: {playlist.Count}");
            Console.WriteLine($"Primera: {playlist.First.Value}");
            Console.WriteLine($"Última: {playlist.Last.Value}");
        }

        public static void DemostrarSortedDictionary()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║    SortedDictionary<TKey, TValue> - Ordenado          ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

            // SortedDictionary: como Dictionary pero mantiene las claves ordenadas
            SortedDictionary<int, string> ranking = new SortedDictionary<int, string>();

            ranking.Add(3, "Bronce");
            ranking.Add(1, "Oro");
            ranking.Add(2, "Plata");

            Console.WriteLine("Ranking (automáticamente ordenado por clave):");
            foreach (var kvp in ranking)
            {
                Console.WriteLine($"  {kvp.Key}. {kvp.Value}");
            }
        }
    }
}
