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
            try {

                // Deserializar JSON a una lista de objetos dinámicos
                List<dynamic> dynamicDataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonFilePath);
                foreach (var dynamicData in dynamicDataList)
                {
                    //Crear el centro
                    centro_educativo centro = JsonACentro(dynamicData);
                    //Crear la provincia 
                    provincia provincia = new provincia
                    {
                        codigo = 30,
                        nombre = "Múrcia"
                    };
                    //Crear localidad
                    localidad localidad = new localidad
                    {
                        nombre = dynamicData.muncen,
                        codigo = 1234
                    };
                    //Insertar centro en la base de datos
                    ConexionBD.insertCentro(centro);
                }
            } 
            catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
            }
            
        }

        public static centro_educativo JsonACentro(dynamic dynamicData) {
            centro_educativo centro = new centro_educativo
            {
                nombre = dynamicData.denCorta + " " + dynamicData.dencen,
                direccion = dynamicData.domcen,
                telefono = dynamicData.telcen,
                descripcion = dynamicData.presentacionCorta,
                cod_postal = dynamicData.cpcen,
            };
            switch (dynamicData.titularidad)
            {
                case "P":
                    centro.tipo = tipo_centro.Público;
                    break;
                case "N":
                    centro.tipo = tipo_centro.Privado;
                    break;
                case "C":
                    centro.tipo = tipo_centro.Concertado;
                    break;
            }
            return centro;
        }
    }
}

