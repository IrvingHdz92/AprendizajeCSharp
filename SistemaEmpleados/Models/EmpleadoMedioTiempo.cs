namespace SistemaEmpleados.Models;
public class EmpleadoMedioTiempo : Empleado
{
    public decimal TarifaPorHora { get; set; }
    public int HorasSemanales { get; set; }


    public EmpleadoMedioTiempo()
    {
        
    }

    public EmpleadoMedioTiempo(string nombre, int id, decimal tarifaPorHora, int horasSemanales) : this()
    {
        Nombre = nombre;
        Id = id;
        TarifaPorHora = tarifaPorHora;
        HorasSemanales = horasSemanales;
    }

    public override decimal CalcularSalario()
    {
        if (HorasSemanales > 20)
        {
            Console.WriteLine("Solo se permite un máximo de 20 horas");
            return 0m;
        }

        return TarifaPorHora * HorasSemanales;
    }

    public override string ObtenerBeneficios()
    {
        string beneficiosBase = base.ObtenerBeneficios();
        return beneficiosBase + ", Horario flexible";
    }
}

