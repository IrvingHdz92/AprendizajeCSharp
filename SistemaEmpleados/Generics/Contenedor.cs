namespace SistemaEmpleados.Generics
{
    /// <summary>
    /// Ejemplo básico de clase genérica
    /// T puede ser cualquier tipo
    /// </summary>
    /// <typeparam name="T">El tipo de dato que almacenará el contenedor</typeparam>
    public class Contenedor<T>
    {
        // Campo privado del tipo genérico T
        private T _valor;

        // Constructor
        public Contenedor(T valorInicial)
        {
            _valor = valorInicial;
        }

        // Propiedad del tipo T
        public T Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        // Método que retorna el tipo T
        public T ObtenerValor()
        {
            return _valor;
        }

        // Método que acepta parámetro del tipo T
        public void EstablecerValor(T nuevoValor)
        {
            _valor = nuevoValor;
        }

        // Método que muestra información
        public void MostrarInformacion()
        {
            Console.WriteLine($"Tipo almacenado: {typeof(T).Name}");
            Console.WriteLine($"Valor: {_valor}");
            Console.WriteLine($"Es tipo por valor: {typeof(T).IsValueType}");
            Console.WriteLine($"Es clase: {typeof(T).IsClass}");
        }
    }
}
