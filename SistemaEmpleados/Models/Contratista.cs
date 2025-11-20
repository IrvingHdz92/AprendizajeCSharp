namespace SistemaEmpleados.Models;

public class Contratista : Empleado, IImprimible, IExportable, IValidable
{
    public decimal TarifaPorHora { get; set; }
    public int HorasTrabajadas { get; set; }
    public DateTime FechaFinContrato { get; set; }

    public Contratista()
    {
        FechaFinContrato = DateTime.Now.AddMonths(6);
    }

    public Contratista(string nombre, int id, decimal tarifaPorHora) : this()
    {
        Nombre = nombre;
        Id = id;
        TarifaPorHora = tarifaPorHora;
        HorasTrabajadas = 0;
    }

    public override decimal CalcularSalario()
    {        
        return TarifaPorHora * HorasTrabajadas;
    }

    // SOBREESCRIBIR beneficios (contratistas tienen menos beneficios)
    public override string ObtenerBeneficios()
    {
        return "Seguro médico básico, Sin beneficios adicionales";
    }

    public void RegistrarHoras(int horas)
    {
        if (horas < 0 || horas > 80)
        {
            Console.WriteLine("Las horas deben estar entre 0 y 80 por periodo");
            return;
        }

        HorasTrabajadas += horas;
        Console.WriteLine($"Horas registradas: {horas}");
        Console.WriteLine($"Total acumulado: {HorasTrabajadas} horas");
    }

    public void RenovarContrato(int meses)
    {
        FechaFinContrato = FechaFinContrato.AddMonths(meses);
        Console.WriteLine($"Contrato renovado hasta: {FechaFinContrato:dd/MM/yyyy}");
    }

    public int DiasRestantesContrato()
    {
        TimeSpan diferencia = FechaFinContrato - DateTime.Now;
        return diferencia.Days;
    }

    public override void MostrarInformacion()
    {
        // Llamar al método del padre para mostrar info básica
        base.MostrarInformacion();
        // Agregar información específica
        Console.WriteLine($"Tarifa por Hora: ${TarifaPorHora:N2}");
        Console.WriteLine($"Horas Trabajadas: {HorasTrabajadas}");
        Console.WriteLine($"Fecha de Fin de Contrato: {FechaFinContrato:dd/MM/yyyy}");
        Console.WriteLine($"Días Restantes de Contrato: {DiasRestantesContrato()} días");
        Console.WriteLine($"Tipo: Contratista");
    }

    // Implementación de IImprimible
    public bool PuedeImprimir => !string.IsNullOrWhiteSpace(Nombre) && Id > 0;

    public string GenerarReportePDF()
    {
        return $@"
        ╔════════════════════════════════════════════╗
        ║          REPORTE CONTRATISTA               ║
        ╚════════════════════════════════════════════╝

        ID: {Id}
        Nombre: {Nombre}
        Fecha Ingreso: {FechaIngreso:dd/MM/yyyy}

        --- CONTRATO ---
        Tarifa por Hora: ${TarifaPorHora:N2}
        Horas Trabajadas: {HorasTrabajadas}
        Salario del Periodo: ${CalcularSalario():N2}
        Fin de Contrato: {FechaFinContrato:dd/MM/yyyy}
        Días Restantes: {DiasRestantesContrato()}

        Generado: {DateTime.Now:dd/MM/yyyy HH:mm}
        ";
    }

    public void ImprimirEnConsola()
    {
        Console.WriteLine(GenerarReportePDF());
    }

    // Implementación de IExportable
    public string ExportarAJSON()
    {
        return $@"{{
                ""id"": {Id},
                ""nombre"": ""{Nombre}"",
                ""tipo"": ""Contratista"",
                ""tarifaPorHora"": {TarifaPorHora},
                ""horasTrabajadas"": {HorasTrabajadas},
                ""salarioTotal"": {CalcularSalario()},
                ""finContrato"": ""{FechaFinContrato:yyyy-MM-dd}""
            }}";
    }

    public string ExportarACSV()
    {
        return $"{Id},{Nombre},Contratista,{CalcularSalario()},{DiasRestantesContrato()}";
    }

    public string ExportarAXML()
    {
        return $@"<Empleado>
            <ID>{Id}</ID>
            <Nombre>{Nombre}</Nombre>
            <Tipo>Contratista</Tipo>
            <TarifaPorHora>{TarifaPorHora}</TarifaPorHora>
            <HorasTrabajadas>{HorasTrabajadas}</HorasTrabajadas>
            <SalarioTotal>{CalcularSalario()}</SalarioTotal>
            <FinContrato>{FechaFinContrato:yyyy-MM-dd}</FinContrato>
            </Empleado>";
    }

    public string[] ObtenerEncabezadosCSV()
    {
        return new string[] { "ID", "Nombre", "Tipo", "Salario", "Días Contrato Restantes" };
    }

    // Implementación de IValidable
    public bool EsValido()
    {
        return ObtenerErrores().Count == 0;
    }

    public List<string> ObtenerErrores()
    {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(Nombre))
            errores.Add("El nombre es obligatorio");

        if (Id <= 0)
            errores.Add("El ID debe ser mayor a 0");

        if (TarifaPorHora <= 0)
            errores.Add("La tarifa por hora debe ser mayor a 0");

        if (HorasTrabajadas < 0)
            errores.Add("Las horas trabajadas no pueden ser negativas");

        if (FechaFinContrato <= DateTime.Now)
            errores.Add("La fecha de fin de contrato debe ser futura");

        return errores;
    }

    public bool ValidarCampo(string nombreCampo)
    {
        switch (nombreCampo.ToLower())
        {
            case "nombre":
                return !string.IsNullOrWhiteSpace(Nombre);
            case "id":
                return Id > 0;
            case "tarifaporhora":
                return TarifaPorHora > 0;
            case "horastrabajadas":
                return HorasTrabajadas >= 0;
            default:
                return true;
        }
    }
}

