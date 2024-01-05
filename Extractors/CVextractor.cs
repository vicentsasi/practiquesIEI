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
using System.Threading.Tasks;

namespace practiquesIEI.Extractors
{
    public class CVextractor
    {


        public static async Task<string> LoadJsonDataIntoDatabase(string jsonData, string logs)
        {

            try
            { // Deserializar JSON a una lista de objetos dinámicos
                List<dynamic> dynamicDataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
                List<centro_educativo> ListaCentros = new List<centro_educativo>();
                foreach (var dynamicData in dynamicDataList)
                {
                    centro_educativo centro = JsonACentro(dynamicData, logs);
                    ListaCentros.Add(centro);
                    provincia provincia = new provincia();

                    if (centro != null)
                    {
                        provincia.codigo = centro.cod_postal.ToString().Substring(0,2);
                        if (dynamicData.PROVINCIA != null)
                        {
                            provincia.nombre = dynamicData.PROVINCIA;
                        }
                        else { provincia = null; }
                    }
                    else { provincia = null; }

                    if (provincia != null)
                    {
                        ConexionBD.insertProvincia(provincia, logs);
                    }

                    localidad localidad = new localidad();

                    if (centro != null && provincia != null)
                    {
                        localidad.codigo = centro.cod_postal.ToString().Substring(2,3);
                        centro.loc_codigo = localidad.codigo;
                        localidad.prov_nombre = provincia.nombre;
                        if (dynamicData.LOCALIDAD != null)
                        {
                            localidad.nombre = dynamicData.LOCALIDAD;
                        }
                        else { localidad = null; }
                    }
                    else { localidad = null; }

                    if (provincia != null)
                    {
                        ConexionBD.insertLocalidad(localidad, logs);
                    }

                }
                foreach (var centro in ListaCentros)
                {
                    if (centro != null)
                    {
                        logs += $"Se inserta el centro {centro.nombre}?? \r\n";
                        logs = await ConexionBD.insertCentro(centro, logs);
                    }
                }
            }
            catch (Exception ex)
            {
                logs += $"Error al convertir el JSON a objetos: {ex.Message} \r\n"; 
            }
            return logs;
        }
        static centro_educativo JsonACentro(dynamic dynamicData, string logs)
        {
            try
            {
                centro_educativo centro = new centro_educativo();
                //nombre del centro
                if (dynamicData.DENOMINACION != null)
                {
                    centro.nombre = dynamicData.DENOMINACION;
                }
                else
                {
                    logs +=$"Error: no se puede obtener el nombre del centro\r\n";
                    return null;
                }
                //diereccion
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
                    logs += $"Error: no se puede obtener la direccion de {centro.nombre}\r\n";          
                        return null;
                }
                // codigo postal
                if (dynamicData.CODIGO_POSTAL != null && (dynamicData.CODIGO_POSTAL.ToString().Length == 5 || dynamicData.CODIGO_POSTAL.ToString().Length == 4))
                {
                    if (dynamicData.CODIGO_POSTAL.ToString().Length == 4)
                    {
                        centro.cod_postal = '0' + dynamicData.CODIGO_POSTAL.ToString();
                    }
                    else { centro.cod_postal = dynamicData.CODIGO_POSTAL; }
                }
                else
                {
                    logs+=$"El codigo postal de {centro.nombre} es nulo o no tiene el numero de digitos correspondientes\r\n";
                    return null;
                }

                //telefono
                if (dynamicData.TELEFONO.ToString().Length == 9 || dynamicData.TELEFONO.ToString().Length == 0)
                {
                    if (dynamicData.TELEFONO.ToString().Length == 0) { centro.telefono = 0; }
                    else { centro.telefono = dynamicData.TELEFONO; }
                }
                else
                {
                    logs+=$"El numero de telefono de {centro.nombre} es {dynamicData.TELEFONO.ToString().Length}\r\n";
                    return null;
                }

                centro.descripcion = dynamicData.URL_VA;


               //tipo de centro
                string regimen = dynamicData.REGIMEN;
                switch (regimen)
                {
                    case "PÚB.": centro.tipo = tipo_centro.Público; break;
                    case "PRIV.": centro.tipo = tipo_centro.Privado; break;
                    case "PRIV. CONC.": centro.tipo = tipo_centro.Concertado; break;
                    case "OTROS": centro.tipo = tipo_centro.Otros; break;
                    default:
                        logs+=$"El tipo de centro de {centro.nombre} no corresponde con ninguno de los tipos guardados\r\n";
                        return null;

                }
                GetLatitudyLongitud(centro.direccion, centro);
                //centro.longitud = "22.02";
                //centro.latitud = "22.02";

                return centro;
            }
            catch (Exception ex)
            {
                logs+=$"Error al obtener los datos para el centro: {ex.Message}\r\n";
                return null;
            }
        }

        public static void GetLatitudyLongitud(string direccion, centro_educativo centro)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--headless", "--disable-gpu", "--no-sandbox", "--disable-software-rasterizer", "--disable-dev-shm-usage", "--disable-extensions", "--disable-notifications");
            options.AddArguments("--silent", "--disable-logging", "--log-level=3", "--log-file=log.txt");

            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;  // Esto oculta la ventana de consola

            // Iniciar el navegador Chrome
            using (var driver = new ChromeDriver(driverService, options))
            {
                try
                {
                    // Navegar a la URL de Nominatim para obtener la latitud y longitud
                    driver.Navigate().GoToUrl($"https://nominatim.openstreetmap.org/search?format=json&q={direccion}");

                    // Esperar a que la página se cargue completamente
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                    // Obtener la respuesta en formato JSON
                    string responseJson = driver.FindElement(By.TagName("pre")).Text;

                    // Deserializar la respuesta JSON
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseJson);

                    // Verificar si se obtuvo alguna información de geolocalización
                    if (jsonResponse.Count > 0)
                    {
                        // Obtener las coordenadas (latitud y longitud)
                        centro.latitud = jsonResponse[0].lat;
                        centro.longitud = jsonResponse[0].lon;
                    }
                    else
                    {
                        // Manejar el caso en el que no se encuentre la geolocalización
                        centro.latitud = null;
                        centro.longitud = null;
                        Console.WriteLine("No se ha podido obtener la geolocalización");
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir durante la obtención de la geolocalización
                    centro.latitud = null;
                    centro.longitud = null;
                    Console.WriteLine($"Error al obtener la geolocalización: {ex.Message}");
                }
                finally
                {
                    // Cerrar el navegador
                    driver.Quit();
                }
            }
        }

    }
}


