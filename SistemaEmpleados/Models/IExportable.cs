namespace SistemaEmpleados.Models;

/// <summary>
/// Interface que define la capacidad de exportar datos a diferentes formatos
/// </summary>
public interface IExportable
{
    // Exportar a JSON
    string ExportarAJSON();

    // Exportar a CSV
    string ExportarACSV();

    // Exportar a XML
    string ExportarAXML();

    // Obtener encabezados para CSV
    string[] ObtenerEncabezadosCSV();
}

