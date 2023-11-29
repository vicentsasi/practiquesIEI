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
                        codigo = 308,
                        nombre = "Múrcia"
                    };
                    //Crear localidad
                    localidad localidad = new localidad
                    {
                        nombre = (dynamicData.muncen = null) ? "Sin municipio" : dynamicData.muncen,
                        codigo = 12
                    };
                }
                int i = 0; 
                foreach (var centro in ListaCentros) {
                    i++;
                    Console.WriteLine($"El centro {centro.nombre} + "+ i);
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
                nombre = (dynamicData.denCorta = null) ? "Sin direccion" : dynamicData.denCorta + " " + (dynamicData.dencen = null) ? "" : dynamicData.dencen,
                direccion = (dynamicData.domcen = null) ? "Sin direccion" : dynamicData.domcen,
                telefono = (dynamicData.telcen = null) ? 0 : dynamicData.telcen,
                descripcion = (dynamicData.presentacionCorta == null) ? "Sin descripcion" : dynamicData.presentacionCorta,
                cod_postal = (dynamicData.cpcen = null) ? 00000 : dynamicData.cpcen
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
                default: centro.tipo = tipo_centro.Otros; break;
            }
            return centro;
        }
    }
}

