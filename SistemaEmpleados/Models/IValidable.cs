namespace SistemaEmpleados.Models;

/// <summary>
/// Interface que define la capacidad de validar datos
/// </summary>
public interface IValidable
{
    // Validar si los datos son correctos
    bool EsValido();

    // Obtener lista de errores de validación
    List<string> ObtenerErrores();

    // Validar un campo específico
    bool ValidarCampo(string nombreCampo);
}

