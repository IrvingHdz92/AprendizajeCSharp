namespace SistemaEmpleados.Models;

public class EmpleadoPermanente : Empleado, IImprimible, IExportable, IValidable
{
    public decimal SalarioBase { get; set; }
    public decimal BonoPorAntiguedad { get; set; }

    public EmpleadoPermanente()
    {

    }

    public EmpleadoPermanente(string nombre, int id, decimal salarioBase) : this()
    {
        Nombre = nombre;
        Id = id;
        SalarioBase = salarioBase;
        BonoPorAntiguedad = 0;
    }

    // IMPLEMENTACIÓN OBLIGATORIA del método abstracto
    // override = se está sobreescribiendo el método de Empleado
    public override decimal CalcularSalario()
    {
        int antiguedad = ObtenerAntiguedad();
        BonoPorAntiguedad = SalarioBase * (antiguedad * 0.02m);

        return SalarioBase + BonoPorAntiguedad;
    }

    // SOBREESCRIBIR método virtual (OPCIONAL)
    // base.ObtenerBeneficios() llama al método del padre
    public override string ObtenerBeneficios()
    {
        // Obtener beneficios básicos del padre
        string beneficiosBase = base.ObtenerBeneficios();

        // Agregar beneficios adicionales
        return beneficiosBase + ", Fondo de ahorro, Vacaciones pagadas, Seguro de vida";
    }

    // Método adicional SOLO para EmpleadoPermanente
    public void SolicitarAumentoSalarial(decimal porcentaje)
    {
        if (porcentaje < 0 || porcentaje > 50)
        {
            Console.WriteLine("El porcentaje debe estar entre 0% y 50%");
        }

        decimal aumento = SalarioBase * (porcentaje / 100);
        SalarioBase += aumento;
        Console.WriteLine($"Aumento aplicado: ${aumento:N2}");
        Console.WriteLine($"Nuevo salario base: ${SalarioBase:N2}");
    }

    // SOBREESCRIBIR MostrarInformacion para agregar datos específicos
    public override void MostrarInformacion()
    {
        // Llamar al método del padre para mostrar info básica
        base.MostrarInformacion();

        // Agregar información específica
        Console.WriteLine($"Salario Base: ${SalarioBase:N2}");
        Console.WriteLine($"Bono por Antigüedad: ${BonoPorAntiguedad:N2}");
        Console.WriteLine($"Tipo: Empleado Permanente");
    }

    public bool PuedeImprimir => true;

    public string GenerarReportePDF()
    {
        // Simulación de generación de PDF
        return $@"
            ╔════════════════════════════════════════════╗
            ║       REPORTE EMPLEADO PERMANENTE          ║
            ╚════════════════════════════════════════════╝

            ID: {Id}
            Nombre: {Nombre}
            Fecha Ingreso: {FechaIngreso:dd/MM/yyyy}
            Años de Servicio: {ObtenerAntiguedad()}

            --- SALARIO ---
            Salario Base: ${SalarioBase:N2}
            Bono Antigüedad: ${BonoPorAntiguedad:N2}
            Salario Total: ${CalcularSalario():N2}

            --- BENEFICIOS ---
            {ObtenerBeneficios()}

            Generado: {DateTime.Now:dd/MM/yyyy HH:mm}
            ";
    }

    public void ImprimirEnConsola()
    {
        Console.WriteLine(GenerarReportePDF());
    }

    public string ExportarAJSON()
    {
        // Formato JSON simple (sin librerías)
        return $@"{{
            ""id"": {Id},
            ""nombre"": ""{Nombre}"",
            ""tipo"": ""EmpleadoPermanente"",
            ""fechaIngreso"": ""{FechaIngreso:yyyy-MM-dd}"",
            ""salarioBase"": {SalarioBase},
            ""bonoPorAntiguedad"": {BonoPorAntiguedad},
            ""salarioTotal"": {CalcularSalario()},
            ""añosServicio"": {ObtenerAntiguedad()}
        }}";
    }

    public string ExportarACSV()
    {
        // Formato CSV: ID,Nombre,Tipo,Salario,Años
        return $"{Id},{Nombre},Permanente,{CalcularSalario()},{ObtenerAntiguedad()}";
    }

    public string ExportarAXML()
    {
        return $@"<Empleado>
            <ID>{Id}</ID>
            <Nombre>{Nombre}</Nombre>
            <Tipo>EmpleadoPermanente</Tipo>
            <FechaIngreso>{FechaIngreso:yyyy-MM-dd}</FechaIngreso>
            <SalarioBase>{SalarioBase}</SalarioBase>
            <BonoPorAntiguedad>{BonoPorAntiguedad}</BonoPorAntiguedad>
            <SalarioTotal>{CalcularSalario()}</SalarioTotal>
        </Empleado>";
    }

    public string[] ObtenerEncabezadosCSV()
    {
        return new string[] { "ID", "Nombre", "Tipo", "Salario", "Años de Servicio" };
    }

    // ========== IMPLEMENTACIÓN DE IValidable ==========

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

        if (SalarioBase <= 0)
            errores.Add("El salario base debe ser mayor a 0");

        if (FechaIngreso > DateTime.Now)
            errores.Add("La fecha de ingreso no puede ser futura");

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
            case "salariobase":
                return SalarioBase > 0;
            case "fechaingreso":
                return FechaIngreso <= DateTime.Now;
            default:
                return true;
        }
    }

}

