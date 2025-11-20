namespace SistemaEmpleados.Models;

public class Pasante : Empleado
{
    // PROPIEDADES ESPECÍFICAS
    public decimal EstipendioMensual { get; set; }
    public string Universidad { get; set; }
    public string Carrera { get; set; }
    public int MesesPasantia { get; set; }

    // CONSTRUCTOR
    public Pasante()
    {
        // Pasantías típicamente duran 6 meses
        MesesPasantia = 6;
    }

    public Pasante(string nombre, int id, decimal estipendio, string universidad) : this()
    {
        Nombre = nombre;
        Id = id;
        EstipendioMensual = estipendio;
        Universidad = universidad;
    }

    public override decimal CalcularSalario()
    {
        return EstipendioMensual;
    }

    public override string ObtenerBeneficios()
    {
        string beneficiosBase = base.ObtenerBeneficios();
        return beneficiosBase + ", Capacitación profesional, Mentoría";
    }

    public void ExtenderPasantia(int mesesAdicionales)
    {
        if (mesesAdicionales < 1 || mesesAdicionales > 6)
        {
            Console.WriteLine("Solo se puede extender entre 1 y 6 meses");
            return;
        }

        MesesPasantia += mesesAdicionales;
        Console.WriteLine($"Pasantía extendida. Duración total: {MesesPasantia} meses");
    }

    public void AsignarProyecto(string nombreProyecto)
    {
        Console.WriteLine($"Pasante {Nombre} asignado al proyecto: {nombreProyecto}");
    }

    public bool PuedeConvertirseEnEmpleado()
    {
        // Criterio: al menos 3 meses de pasantía        
        return ObtenerAntiguedad() > 0 || (DateTime.UtcNow - FechaIngreso).Days >= 90;
    }

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine($"Estipendio Mensual: ${EstipendioMensual:N2}");
        Console.WriteLine($"Universidad: {Universidad}");
        Console.WriteLine($"Carrera: {Carrera}");
        Console.WriteLine($"Duración Pasantía: {MesesPasantia} meses");
        Console.WriteLine($"Puede ser empleado: {(PuedeConvertirseEnEmpleado() ? "Sí" : "No")}");
        Console.WriteLine($"Tipo: Pasante");
    }
}

