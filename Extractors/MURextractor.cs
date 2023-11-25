using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using practiquesIEI.Entities;

namespace practiquesIEI.Extractors
{
    public class MURextractor
    {

        
        public static void LoadJsonDataIntoDatabase(string jsonFilePath)
        {
            string jsonData = jsonFilePath;

            // Deserializar JSON a una lista de objetos dinámicos
            List<dynamic> dynamicDataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            foreach (var dynamicData in dynamicDataList)
            {
                centro_educativo centro = new centro_educativo();
                localidad local = new localidad();
                provincia provincia = new provincia();  

                // Extraer propiedades específicas y construir las columnas que deseas
                centro.nombre = dynamicData.dencen;
                centro.direccion = dynamicData.domcen;
                centro.tipo = dynamicData.titularidad;
                centro.telefono = dynamicData.telcen;
                centro.descripcion = dynamicData.presentacionCorta;
                centro.cod_postal = dynamicData.cpcen;
                //se te que mirar el mapping per a extraure els datos

                /*
                 * Insertar centro en la base de datos
                 * centro_educativo centroPr = new centro_educativo(datos que traus del JSON);
                 * ConexionBD.insertCentro(centroPr);
                */
                
            }
        }

        public static centro_educativo JsonACentro() { }

    }
}

