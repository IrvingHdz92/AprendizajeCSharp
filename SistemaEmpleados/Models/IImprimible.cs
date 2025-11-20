namespace SistemaEmpleados.Models;

/// <summary>
/// Interface que define la capacidad de imprimir información en formato PDF
/// </summary>
public interface IImprimible
{
    // MÉTODOS(sin implementación, sin cuerpo)
    string GenerarReportePDF();

    void ImprimirEnConsola();

    // PROPIEDADES (solo declaración)
    bool PuedeImprimir { get; }
}

