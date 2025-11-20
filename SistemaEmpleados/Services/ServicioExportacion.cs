namespace SistemaEmpleados.Services
{
    using SistemaEmpleados.Models;
    using System.IO;

    /// <summary>
    /// Servicio que trabaja con CUALQUIER objeto que implemente IExportable
    /// Ejemplo de COMPOSICIÓN: usa capacidades, no herencia
    /// </summary>
    public class ServicioExportacion
    {
        // Este método acepta CUALQUIER cosa que implemente IExportable
        public void ExportarEmpleadosAJSON(List<IExportable> empleados, string nombreArchivo)
        {
            Console.WriteLine($"\n=== Exportando {empleados.Count} empleados a JSON ===");

            var contenido = "[\n";
            for (int i = 0; i < empleados.Count; i++)
            {
                contenido += empleados[i].ExportarAJSON();
                if (i < empleados.Count - 1)
                    contenido += ",\n";
            }
            contenido += "\n]";

            // Guardar en archivo
            string ruta = Path.Combine(Environment.CurrentDirectory, nombreArchivo);
            File.WriteAllText(ruta, contenido);

            Console.WriteLine($"✓ Archivo guardado: {ruta}");
        }

        public void ExportarEmpleadosACSV(List<IExportable> empleados, string nombreArchivo)
        {
            Console.WriteLine($"\n=== Exportando {empleados.Count} empleados a CSV ===");

            var lineas = new List<string>();

            // Encabezados (tomamos del primer empleado)
            if (empleados.Count > 0)
            {
                string[] encabezados = empleados[0].ObtenerEncabezadosCSV();
                lineas.Add(string.Join(",", encabezados));
            }

            // Datos
            foreach (var empleado in empleados)
            {
                lineas.Add(empleado.ExportarACSV());
            }

            string contenido = string.Join("\n", lineas);

            // Guardar en archivo
            string ruta = Path.Combine(Environment.CurrentDirectory, nombreArchivo);
            File.WriteAllText(ruta, contenido);

            Console.WriteLine($"✓ Archivo guardado: {ruta}");
        }

        public void ExportarEmpleadosAXML(List<IExportable> empleados, string nombreArchivo)
        {
            Console.WriteLine($"\n=== Exportando {empleados.Count} empleados a XML ===");

            var contenido = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<Empleados>\n";

            foreach (var empleado in empleados)
            {
                contenido += empleado.ExportarAXML() + "\n";
            }

            contenido += "</Empleados>";

            // Guardar en archivo
            string ruta = Path.Combine(Environment.CurrentDirectory, nombreArchivo);
            File.WriteAllText(ruta, contenido);

            Console.WriteLine($"✓ Archivo guardado: {ruta}");
        }
    }
}
