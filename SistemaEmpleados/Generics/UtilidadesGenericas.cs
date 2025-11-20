namespace SistemaEmpleados.Generics
{
    /// <summary>
    /// Clase con métodos genéricos estáticos
    /// Los métodos pueden ser genéricos sin que la clase lo sea
    /// </summary>
    public static class UtilidadesGenericas
    {
        // Método genérico para intercambiar dos valores
        public static void Intercambiar<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        // Método genérico para imprimir un array
        public static void ImprimirArray<T>(T[] array, string titulo = "Array")
        {
            Console.WriteLine($"\n=== {titulo} ===");
            Console.WriteLine($"Tipo: {typeof(T).Name}");
            Console.WriteLine($"Elementos: {array.Length}");
            Console.Write("Contenido: [ ");

            foreach (T elemento in array)
            {
                Console.Write($"{elemento} ");
            }

            Console.WriteLine("]");
        }

        // Método genérico para encontrar el primer elemento que cumple una condición
        public static T BuscarPrimero<T>(T[] array, Func<T, bool> condicion)
        {
            foreach (T elemento in array)
            {
                if (condicion(elemento))
                    return elemento;
            }

            return default(T); // default(T) retorna el valor por defecto del tipo
        }

        // Método genérico para contar elementos que cumplen una condición
        public static int Contar<T>(T[] array, Func<T, bool> condicion)
        {
            int contador = 0;

            foreach (T elemento in array)
            {
                if (condicion(elemento))
                    contador++;
            }

            return contador;
        }

        // Método genérico para convertir un array a otro tipo
        public static TOutput[] Convertir<TInput, TOutput>(
            TInput[] array,
            Func<TInput, TOutput> conversor)
        {
            TOutput[] resultado = new TOutput[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                resultado[i] = conversor(array[i]);
            }

            return resultado;
        }

        // Método genérico que muestra el tipo y valor por defecto
        public static void MostrarInformacionTipo<T>()
        {
            Console.WriteLine($"\n=== Información de Tipo: {typeof(T).Name} ===");
            Console.WriteLine($"Nombre completo: {typeof(T).FullName}");
            Console.WriteLine($"Es tipo por valor: {typeof(T).IsValueType}");
            Console.WriteLine($"Es clase: {typeof(T).IsClass}");
            Console.WriteLine($"Es interface: {typeof(T).IsInterface}");
            Console.WriteLine($"Es enum: {typeof(T).IsEnum}");
            Console.WriteLine($"Valor por defecto: {default(T)}");
        }

        // Método genérico para comparar dos valores
        public static bool SonIguales<T>(T valor1, T valor2)
        {
            // EqualityComparer es la forma correcta de comparar genéricos
            return EqualityComparer<T>.Default.Equals(valor1, valor2);
        }

        // Método genérico para crear una lista con elementos duplicados
        public static List<T> Repetir<T>(T elemento, int veces)
        {
            var lista = new List<T>();

            for (int i = 0; i < veces; i++)
            {
                lista.Add(elemento);
            }

            return lista;
        }
    }
}
