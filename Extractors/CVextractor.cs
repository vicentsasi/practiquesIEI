using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using practiquesIEI.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Windows.Forms;
using OpenQA.Selenium.DevTools.V117.Debugger;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace practiquesIEI.Extractors
{
    public class CVextractor
    {
        public static void LoadJsonDataIntoDatabase(string jsonData)
        {
            try
            { // Deserializar JSON a una lista de objetos dinámicos
                List<dynamic> dynamicDataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
                List<centro_educativo> ListaCentros = new List<centro_educativo>();
                foreach (var dynamicData in dynamicDataList)
                {
                    centro_educativo centro = JsonACentro(dynamicData);
                    ListaCentros.Add(centro);
                    provincia provincia = new provincia();

                    if (centro != null)
                    {
                        provincia.codigo = int.Parse(centro.cod_postal.ToString().Substring(0, 2));
                        if (dynamicData.PROVINCIA != null)
                        {
                            provincia.nombre = dynamicData.PROVINCIA;
                        }
                        else { provincia = null; }
                    }
                    else { provincia = null; }

                    if (provincia != null)
                    {
                        ConexionBD.insertProvincia(provincia);
                    }

                    localidad localidad = new localidad();

                    if (centro != null)
                    {
                        localidad.codigo = int.Parse(centro.cod_postal.ToString().Substring(2, 4));
                        if (dynamicData.LOCALIDAD != null)
                        {
                            localidad.nombre = dynamicData.LOCALIDAD;
                        }
                        else { localidad = null; }
                    }
                    else { localidad = null; }

                    if (provincia != null)
                    {
                        ConexionBD.insertLocalidad(localidad);
                    }

                }
                foreach (var centro in ListaCentros)
                {
                    if (centro != null)
                    {
                        Console.WriteLine($"Se inserta el centro {centro.nombre}??");
                        ConexionBD.insertCentro(centro);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir el JSON a objetos: {ex.Message}");
            }

        }
        static centro_educativo JsonACentro(dynamic dynamicData)
        {
            try
            {
                centro_educativo centro = new centro_educativo();
                // Extraer propiedades específicas y construir las columnas que deseas

                if (dynamicData.TIPO_VIA != null && dynamicData.DIRECCION != null && dynamicData.NUMERO != null)
                {
                    string tipoVia = dynamicData.TIPO_VIA;
                    string direccion = dynamicData.DIRECCION;
                    string numero = dynamicData.NUMERO;
                    // Concatenar los valores en una sola cadena para la columna "Direccion"
                    centro.direccion = $"{tipoVia} {direccion} {numero}";
                }
                else
                {
                    return null;
                }

                if (dynamicData.DENOMINACION != null)
                {
                    centro.nombre = dynamicData.DENOMINACION;
                }
                else
                {
                    return null;
                }

                if (dynamicData.CODIGO_POSTAL != null && (dynamicData.CODIGO_POSTAL.ToString().Length == 5 || dynamicData.CODIGO_POSTAL.ToString().Length == 4))
                {
                    if (dynamicData.CODIGO_POSTAL.ToString().Length == 4)
                    {
                        centro.cod_postal = int.Parse(dynamicData.CODIGO_POSTAL.ToString("D2"));
                    }
                    else { centro.cod_postal = dynamicData.CODIGO_POSTAL; }
                }
                else
                {
                    Console.WriteLine($"El codigo postal de {centro.nombre = dynamicData.denCorta + " " + dynamicData.dencen} es nulo o no tiene el numero de digitos correspondientes ");
                    return null;
                }

                //telefono
                if (dynamicData.TELEFONO != null && dynamicData.TELEFONO.ToString().Length == 9)
                {
                    centro.telefono = dynamicData.TELEFONO;
                }
                else
                {
                    Console.WriteLine($"El numero de telefono de {centro.nombre = dynamicData.denCorta + " " + dynamicData.dencen} es nulo o no tiene 9 digitos ");
                    return null;
                }

                centro.descripcion = dynamicData.URL_VA;


                if (dynamicData.REGIMEN != null)
                {
                    string regimen = dynamicData.REGIMEN;
                    switch (regimen)
                    {
                        case "PÚB": centro.tipo = tipo_centro.Público; break;
                        case "PRIV": centro.tipo = tipo_centro.Privado; break;
                        case "PRIV.CONC": centro.tipo = tipo_centro.Concertado; break;
                        case "OTROS": centro.tipo = tipo_centro.Otros; break;
                    }
                }
                else { return null; }

                 centro.longitud = GetLongitud(centro.direccion).Replace(",",".");
                centro.latitud = GetLatitud(centro.direccion).Replace(",",".");

            
                return centro;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir el JSON a objeto centro: {ex.Message}");
                return null;
            }
        }

        private static string GetLatitud(string address)
        {
            try
            {
                //ChromeOptions options = new ChromeOptions();
                //options.AddArgument("--headless");
                 

                using (var driver = new ChromeDriver())
                {
                    driver.Navigate().GoToUrl($"https://www.coordenadas-gps.com");

                    // Esperar un tiempo fijo para dar tiempo a que la página cargue
                    System.Threading.Thread.Sleep(2000);

                    // Ingresa la dirección
                    IWebElement addressInput = driver.FindElement(By.Id("address"));
                    addressInput.SendKeys(address);

                    // Haz clic en el botón
                    driver.FindElement(By.CssSelector("button.btn.btn-primary[onclick='codeAddress()']")).Click();

                    // Esperar un tiempo fijo para dar tiempo a que la latitud se actualice
                    System.Threading.Thread.Sleep(2000);

                    // Obtener y devolver el valor de latitud
                    IWebElement latInput = driver.FindElement(By.Id("latitude"));
                    string longitud = latInput.GetAttribute("value");
                    longitud = longitud.Replace(".", ",");
                    return longitud;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la latitud: {ex.Message}");
                return null;
            }
        }


        private static string GetLongitud(string address)
        {
            try
            {
                //ChromeOptions options = new ChromeOptions();
                //options.AddArgument("--headless"); // Ejecutar en modo sin cabeza (headless)
                using (var driver = new ChromeDriver())
                {
                    driver.Navigate().GoToUrl($"https://www.coordenadas-gps.com");

                    // Esperar un tiempo fijo para dar tiempo a que la página cargue
                    System.Threading.Thread.Sleep(2000);

                    // Ingresa la dirección
                    IWebElement addressInput = driver.FindElement(By.Id("address"));
                    addressInput.SendKeys(address);

                    // Haz clic en el botón
                    driver.FindElement(By.CssSelector("button.btn.btn-primary[onclick='codeAddress()']")).Click();

                    // Esperar un tiempo fijo para dar tiempo a que la latitud se actualice
                    System.Threading.Thread.Sleep(2000);

                    // Obtener y devolver el valor de latitud
                    IWebElement latInput = driver.FindElement(By.Id("longitude"));
                    string longitud = latInput.GetAttribute("value");
                    longitud = longitud.Replace(".", ",");
                    return longitud;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la latitud: {ex.Message}");
                return null;
            }

        }
    }
}


