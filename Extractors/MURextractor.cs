using System;
using System.Collections.Generic;
using System.Data;
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
                List<centro_educativo> ListaCentros = new List<centro_educativo>();
                foreach (var dynamicData in dynamicDataList)
                {
                    ListaCentros.Add(JsonACentro(dynamicData));
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
                }
                foreach (var centro in ListaCentros) {
                    Console.WriteLine($"El centro {centro.nombre} se inserta???");
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
                /*direccion = dynamicData.domcen,
                telefono = dynamicData.telcen,
                descripcion = dynamicData.presentacionCorta,
                cod_postal = dynamicData.cpcen,*/
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

