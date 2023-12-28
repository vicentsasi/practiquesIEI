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
using System.Threading.Tasks;

namespace practiquesIEI.Extractors
{
    public class MURextractor
    {
        public static async Task<string> LoadJsonDataIntoDatabase(string jsonFilePath, string logs)
        {
            try
            {
                // Deserializar JSON a una lista de objetos dinámicos
                List<dynamic> dynamicDataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonFilePath);
                List<centro_educativo> ListaCentros = new List<centro_educativo>();
                foreach (var dynamicData in dynamicDataList)
                {
                    centro_educativo centro = JsonACentro(dynamicData, logs);
                    ListaCentros.Add(centro);
                    //Crear la provincia 
                    provincia provincia = new provincia();
                    if (centro != null)
                    {
                        provincia.codigo = "30";
                        provincia.nombre = "Múrcia";
                        ConexionBD.insertProvincia(provincia, logs);
                    }
                    //Crear localidad
                    localidad localidad = new localidad();
                    if (centro != null)
                    {
                        if (dynamicData.loccen != null && (dynamicData.cpcen.ToString().Length == 6 || dynamicData.cpcen.ToString().Length == 5))
                        {
                            localidad.codigo = dynamicData.cpcen.ToString().Substring(2, 3);
                            localidad.nombre = dynamicData.loccen;
                            centro.loc_codigo = localidad.codigo;
                        }
                        else
                        {
                            logs += $"El codigo postal o nombre de la localidad del centro es erroneo\n";
                            localidad = null;
                        }
                    }
                    else { localidad = null; }
                    if (localidad != null)
                    {
                        localidad.prov_nombre = provincia.nombre;
                        ConexionBD.insertLocalidad(localidad, logs);
                    }

                }
                foreach (var centro in ListaCentros)
                {
                    if (centro != null)
                    {
                        logs += $"Se inserta el centro {centro.nombre}?\n";
                        ConexionBD.insertCentro(centro, logs);
                    }
                }
            }
            catch (Exception e)
            {
                logs += $"Error: {e.Message}\n";
                return logs;
            }
            return logs;
        }

        public static centro_educativo JsonACentro(dynamic dynamicData, string logs)
        {
            centro_educativo centro = new centro_educativo();
            //nombre del centro
            if (dynamicData.dencen != null)
            {
                centro.nombre = dynamicData.denCorta + " " + dynamicData.dencen;
            }
            else
            {
                logs +=$"El nombre del centro es null\n";
                return null;
            }
            //codigo postal
            if (dynamicData.cpcen.ToString().Length == 5)
            {
                centro.cod_postal = dynamicData.cpcen.ToString();
            }
            else
            {
                logs +=$"El codigo postal de {centro.nombre} es nulo o no tiene el numero de digitos correspondientes \n";
                return null;
            }
            //telefono
            if (dynamicData.telcen.ToString().Length == 9)
            {
                centro.telefono = dynamicData.telcen;
            }
            else
            {
                logs +=$"El numero de telefono de {centro.nombre} es nulo o no tiene 9 digitos \n";
                return null;
            }
            //direccion
            if (dynamicData.domcen != null)
            {
                centro.direccion = dynamicData.domcen;
            }
            else
            {
                logs += $"La direccion del centro {centro.nombre} es nulo o no tiene el numero de digitos correspondientes \n";
                return null;
            }
            //descripcion
            centro.descripcion = dynamicData.presentacionCorta;
            //latitud
            if (dynamicData["geo-referencia"]["lat"] != null)
            {
                centro.latitud = dynamicData["geo-referencia"]["lat"].ToString().Replace(",", ".");
            }
            else
            {
                logs += $"La latitud del centro {centro.nombre} es nulo o no tiene el numero de digitos correspondientes \n";
                return null;
            }
            //longitud
            if (dynamicData["geo-referencia"]["lon"] != null)
            {
                centro.longitud = dynamicData["geo-referencia"]["lon"].ToString().Replace(",", ".");
            }
            else
            {
                logs += $"La longitud del centro {centro.nombre} es nulo o no tiene el numero de digitos correspondientes \n";
                return null;
            }

            //tipo de centro
            if (dynamicData.titularidad != null)
            {
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

