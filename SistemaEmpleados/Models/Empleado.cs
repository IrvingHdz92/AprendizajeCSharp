namespace SistemaEmpleados.Models;
public abstract class Empleado
{
    public string Nombre { get; set; }
    public int Id { get; set; }
    public DateTime FechaIngreso { get; set; }

    protected Empleado()
    {
        FechaIngreso = DateTime.Now;
    }

    // MÉTODO ABSTRACTO
    // abstract = cada clase hija DEBE implementarlo
    // No tiene cuerpo (sin { })
    public abstract decimal CalcularSalario();

    // MÉTODO VIRTUAL
    // virtual = tiene implementación por defecto
    // Las clases hijas PUEDEN cambiarlo si quieren
    public virtual string ObtenerBeneficios()
    {
        return "Seguro médico básico";
    }

    // MÉTODO CONCRETO (normal)
    // No tiene virtual ni abstract = NO se puede modificar en hijas
    public int ObtenerAntiguedad()
    {
        return DateTime.Now.Year - FechaIngreso.Year;
    }

    public virtual void MostrarInformacion()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Fecha de Ingreso: {FechaIngreso.ToShortDateString()}");
        Console.WriteLine($"Antiguedad: {ObtenerAntiguedad()} años");
        Console.WriteLine($"Salario: ${CalcularSalario():N2}");
        Console.WriteLine($"Beneficios: {ObtenerBeneficios()}");
    }
}

