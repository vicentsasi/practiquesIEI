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
                
                localidad local = new localidad();
                provincia provincia = new provincia();

                //Creamos y inicializamos el centro
                centro_educativo centro = JsonACentro(dynamicData);

                //Creamos la provincia 
                provincia.codigo = 30;
                provincia.nombre = "Múrcia";


                local.nombre = dynamicData.muncen;
                local.codigo = 1234;

                /*
                 * Insertar centro en la base de datos
                 * centro_educativo centroPr = new centro_educativo(datos que traus del JSON);
                 * ConexionBD.insertCentro(centroPr);
                */
                
            }
        }

        public static centro_educativo JsonACentro(dynamic dynamicData) {
            centro_educativo centro = new centro_educativo();
            
            centro.nombre = dynamicData.dencen;
            centro.direccion = dynamicData.domcen;
            centro.tipo = dynamicData.titularidad;
            centro.telefono = dynamicData.telcen;
            centro.descripcion = dynamicData.presentacionCorta;
            centro.cod_postal = dynamicData.cpcen;

            switch (dynamicData.titularidad) {
                case "P": centro.tipo = tipo_centro.Público;
                    break;
                case "N": centro.tipo = tipo_centro.Privado;
                    break;
                case "C": centro.tipo = tipo_centro.Concertado;
                    break;
            }

            return centro;
        }

        

    }
}

