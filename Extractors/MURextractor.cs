using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using practiquesIEI.Entities;
using OpenQA.Selenium.Support.UI;




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
                    ConexionBD.insertProvincia(provincia);
                    //Crear localidad
                    localidad localidad = new localidad
                    {
                        nombre = dynamicData.muncen,
                        codigo = int.Parse(dynamicData.cpcen.ToString()) % 1000
                    };
                    ConexionBD.insertLocalidad(localidad);
                }
                foreach (var centro in ListaCentros) {
                    if (centro != null) {
                        Console.WriteLine($"Se inserta el centro {centro.nombre}??");
                        ConexionBD.insertCentro(centro);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
       
        public static centro_educativo JsonACentro(dynamic dynamicData) {
            centro_educativo centro = new centro_educativo();
            //nombre del centro
            if (dynamicData.dencen != null)
            {
                centro.nombre = dynamicData.denCorta + " " + dynamicData.dencen;
            }
            else
            {
                Console.WriteLine($"El nombre del centro es null");
                return null;
            }
            //codigo postal
            if (dynamicData.cpcen != null && (dynamicData.cpcen.ToString().Length == 6 || dynamicData.cpcen.ToString().Length == 5))
            {
                if (dynamicData.cpcen.ToString().Length == 5) {
                    centro.cod_postal = int.Parse(dynamicData.cpcen.ToString("D2")); 
                }
                else { centro.cod_postal = dynamicData.cpcen; }
            }
            else
            {
                Console.WriteLine($"El codigo postal de {centro.nombre = dynamicData.denCorta + " " + dynamicData.dencen} es nulo o no tiene el numero de digitos correspondientes ");
                return null;
            }
            //telefono
            if (dynamicData.telcen != null && dynamicData.telcen.ToString().Length == 9)
            {
                centro.telefono = dynamicData.telcen;
            }
            else
            {
                Console.WriteLine($"El numero de telefono de {centro.nombre = dynamicData.denCorta + " " + dynamicData.dencen} es nulo o no tiene 9 digitos ");
                return null;
            }
            //direccion
            if (dynamicData.domcen != null) { 
                centro.direccion = dynamicData.domcen;
            }
            else
            {
                Console.WriteLine($"La direccion del centro es null");
                return null;
            }
            //descripcion
            centro.descripcion = dynamicData.presentacionCorta;
            //latitud
            if (dynamicData["geo-referencia"]["lat"] != null) { 

                centro.latitud = decimal.Parse(dynamicData["geo-referencia"]["lat"].ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                Console.WriteLine($"La latitud del centro es null");
                return null;
            }
            //longitud
            if (dynamicData["geo-referencia"]["lon"] != null) {
                centro.longitud = decimal.Parse(dynamicData["geo-referencia"]["lon"].ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                Console.WriteLine($"La longitud del centro es null");
                return null;
            }

            //tipo de centro
            if (dynamicData.titularidad != null) {
                string tipo = dynamicData.titularidad;
                switch (tipo)
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
                    /*default:
                        return null;*/
                }
            }
            else { return null; }
            return centro;
        }
    }
}

