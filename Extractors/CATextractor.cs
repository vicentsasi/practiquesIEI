using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using Aspose.Cells;
using Newtonsoft.Json;
using practiquesIEI.Entities;

namespace practiquesIEI.Extractors
{
    public class CATextractor
    {
        public static void LoadJsonDataIntoDatabase(string jsonData)
        {
            try {
                // Deserializar JSON a una lista de objetos dinámicos
                dynamic jsontext = JsonConvert.DeserializeObject<dynamic>(jsonData);
                List<centro_educativo> ListaCentros = new List<centro_educativo>();
                foreach (var row in jsontext.response.row.row)
                {
                    centro_educativo centro = JsonACentro(row);
                    ListaCentros.Add(centro);
                    provincia provincia = new provincia();

                    if (centro != null)
                    {
                        provincia.codigo = int.Parse(centro.cod_postal.ToString().Substring(0, 2));
                        if (provincia.codigo != 0)
                        {
                            switch (provincia.codigo) {
                                case 8: provincia.nombre = "Barcelona"; break;
                                case 17: provincia.nombre = "Girona"; break;
                                case 43: provincia.nombre = "Tarragona"; break;
                                case 25: provincia.nombre = "Lleida"; break;
                            }
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
                        localidad.codigo = row.codi_municipi_5_digits;
                        if (row.nom_municipi != null)
                        {
                            localidad.nombre = row.nom_municipi;
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
        static centro_educativo JsonACentro(dynamic row)
        {
            try
            {
                centro_educativo centro = new centro_educativo();
                // Extraer propiedades específicas y construir las columnas que deseas

                if (row.adre_a != null)
                {
                    
                    // Concatenar los valores en una sola cadena para la columna "Direccion"
                    centro.direccion = row.adre_a;
                }
                else
                {
                    return null;
                }

                if (row.denominaci_completa != null)
                {
                    centro.nombre = row.denominaci_completa;
                }
                else
                {
                    return null;
                }

                if (row.codi_postal != null && (row.codi_postal.ToString().Length == 5 || row.codi_postal.ToString().Length == 4))
                {
                    if (row.codi_postal.ToString().Length == 4)
                    {
                        centro.cod_postal = int.Parse(row.codi_postal.ToString("D2"));
                    }
                    else { centro.cod_postal = row.codi_postal; }
                }
                else
                {
                    Console.WriteLine($"El codigo postal de {centro.nombre = row.denCorta + " " + row.dencen} es nulo o no tiene el numero de digitos correspondientes ");
                    return null;
                }

                //telefono
                centro.telefono = 0;

                centro.descripcion = row.estudis;


                if (row.nom_naturalesa != null)
                {
                    string regimen = row.nom_naturalesa;
                    switch (regimen)
                    {
                        case "Públic": centro.tipo = tipo_centro.Público; break;
                        case "Privat": centro.tipo = tipo_centro.Privado; break;
                        default: centro.tipo = tipo_centro.Otros; break;
                    }
                }
                else { return null; }

                if (row.coordenades_geo_x != null)
                {
                    centro.longitud = row.coordenades_geo_x;
                }
                else
                {
                    return null;
                }


                if (row.coordenades_geo_y != null)
                {
                    centro.latitud = row.coordenades_geo_y;
                }
                else
                {
                    return null;
                }


                return centro;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir el JSON a objeto centro: {ex.Message}");
                return null;
            }
        }

    }
}

