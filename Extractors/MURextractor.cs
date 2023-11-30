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
                    centro_educativo centro = JsonACentro(dynamicData);
                    ListaCentros.Add(centro);
                    //Crear la provincia 
                    if (centro != null)
                    {
                        provincia provincia = new provincia
                        {
                            codigo = 30,
                            nombre = "Múrcia"
                        };
                        ConexionBD.insertProvincia(provincia);
                    }
                    //Crear localidad
                    localidad localidad = new localidad();
                    if (centro != null)
                    {
                        if (dynamicData.loccen != null && (dynamicData.cpcen.ToString().Length == 6 || dynamicData.cpcen.ToString().Length == 5))
                        {
                            localidad.codigo = int.Parse(dynamicData.cpcen.ToString().Substring(2, 3));
                            localidad.nombre = dynamicData.loccen;
                        }
                        else
                        {
                            Console.WriteLine($"El codigo postal o nombre de la localidad del centro es erroneo");
                            localidad = null;
                        }
                    }
                    else { localidad = null; }
                    if (localidad != null) { ConexionBD.insertLocalidad(localidad); }
                   
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
                    centro.cod_postal = '0' + dynamicData.cpcen.ToString(); 
                }
                else { centro.cod_postal = dynamicData.cpcen; }
            }
            else
            {
                Console.WriteLine($"El codigo postal de {centro.nombre} es nulo o no tiene el numero de digitos correspondientes ");
                return null;
            }
            //telefono
            if (dynamicData.telcen.ToString().Length == 9)
            {
                centro.telefono = dynamicData.telcen;
            }
            else
            {
                Console.WriteLine($"El numero de telefono de {centro.nombre} es nulo o no tiene 9 digitos ");
                return null;
            }
            //direccion
            if (dynamicData.domcen != null) { 
                centro.direccion = dynamicData.domcen;
            }
            else
            {
                Console.WriteLine($"La direccion del centro {centro.nombre} es null");
                return null;
            }
            //descripcion
            centro.descripcion = dynamicData.presentacionCorta;
            //latitud
            if (dynamicData["geo-referencia"]["lat"] != null) {
                centro.latitud = dynamicData["geo-referencia"]["lat"].ToString().Replace(",",".");
            }
            else
            {
                Console.WriteLine($"La latitud del centro {centro.nombre} es null");
                return null;
            }
            //longitud
            if (dynamicData["geo-referencia"]["lon"] != null) {
                centro.longitud = dynamicData["geo-referencia"]["lon"].ToString().Replace(",", ".");
            }
            else
            {
                Console.WriteLine($"La longitud del centro {centro.nombre} es null");
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
                    default:
                        return null;
                }
            }
            else { return null; }
            return centro;
        }
    }
}

