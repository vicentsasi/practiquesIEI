using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace practiquesIEI.Extractors
{
    public class MURextractor
    {
        public static void LoadJsonDataIntoDatabase(string jsonFilePath)
        {
            string jsonData = File.ReadAllText(jsonFilePath);

            // Deserializar JSON a una lista de objetos dinámicos
            List<dynamic> dynamicDataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            foreach (var dynamicData in dynamicDataList)
            {
                // Extraer propiedades específicas y construir las columnas que deseas
                string tipoVia = dynamicData.Tipo_via;
                string direccion = dynamicData.Direccion;
                string numero = dynamicData.Numero;

                // Concatenar los valores en una sola cadena para la columna "Direccion"
                string direccionCompleta = $"{tipoVia} {direccion} {numero}";

                // Extraer el código postal para la columna "CodigoPostal"
                string codigoPostal = dynamicData.Codigo_postal;

                // Luego, puedes usar estas columnas para insertar en la base de datos
                InsertIntoDatabase(direccionCompleta, codigoPostal);
            }
        }

        private static void InsertIntoDatabase(string direccionCompleta, string codigoPostal)
        {
            // Aquí implementa la lógica para insertar las columnas en la base de datos
            // Puedes utilizar Entity Framework u otro método según tu elección
            // y la estructura de tu base de datos
            // Ejemplo: using (var context = new ApplicationDbContext()) { ... }
            Console.WriteLine($"Insertando en la base de datos: Direccion={direccionCompleta}, CodigoPostal={codigoPostal}");
        }
    }
}

