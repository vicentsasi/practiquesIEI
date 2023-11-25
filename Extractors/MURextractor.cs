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
                string nombre = dynamicData.dencen;
                string direccion = dynamicData.domcen;
                string tipo = dynamicData.titularidad;
                //se te que mirar el mapping per a extraure els datos

                /*
                 * Insertar centro en la base de datos
                 * centro_educativo centroPr = new centro_educativo(datos que traus del JSON);
                 * ConexionBD.insertCentro(centroPr);
                */
                
            }
        }
    }
}

