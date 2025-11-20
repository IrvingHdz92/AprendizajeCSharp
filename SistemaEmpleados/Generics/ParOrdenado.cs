namespace SistemaEmpleados.Generics
{
    // <summary>
    /// Clase genérica con DOS parámetros de tipo
    /// Similar a KeyValuePair<TKey, TValue> de .NET
    /// </summary>
    /// <typeparam name="TPrimero">Tipo del primer elemento</typeparam>
    /// <typeparam name="TSegundo">Tipo del segundo elemento</typeparam>
    public class ParOrdenado<TPrimero, TSegundo>
    {
        public TPrimero Primero { get; set; }
        public TSegundo Segundo { get; set; }

        // Constructor
        public ParOrdenado(TPrimero primero, TSegundo segundo)
        {
            Primero = primero;
            Segundo = segundo;
        }

        // Método que retorna tupla
        public (TPrimero, TSegundo) ObtenerTupla()
        {
            return (Primero, Segundo);
        }

        // Intercambiar valores
        public ParOrdenado<TSegundo, TPrimero> Intercambiar()
        {
            return new ParOrdenado<TSegundo, TPrimero>(Segundo, Primero);
        }

        public override string ToString()
        {
            return $"({Primero}, {Segundo})";
        }

        // Comparar si ambos elementos son iguales
        public bool SonIguales()
        {
            // Usar EqualityComparer para comparación segura
            return EqualityComparer<TPrimero>.Default.Equals(Primero, default(TPrimero)) == false &&
                   EqualityComparer<TSegundo>.Default.Equals(Segundo, default(TSegundo)) == false;
        }
    }
}
