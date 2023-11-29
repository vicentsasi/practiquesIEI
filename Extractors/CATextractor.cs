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

                foreach (var row in jsontext.response.row.row)
                {
                    centro_educativo centro = new centro_educativo();

                    centro.nombre = row.denominaci_completa;

                    switch (row.nom_naturalesa)
                    {
                        case "Public": centro.tipo = tipo_centro.Público; break;
                        case "Privat": centro.tipo = tipo_centro.Privado; break;
                    }

                    centro.direccion = row.adre_a;
                    centro.cod_postal = row.codi_postal;
                    centro.longitud = row.coordenades_geo_x;
                    centro.latitud = row.coordenades_geo_y;
                    centro.descripcion = row.estudis;

                    localidad local = new localidad();

                    local.nombre = row.nom_municipi;
                    local.codigo = row.codi_municipi_5_digits;

                    provincia prov = new provincia();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir el JSON a objetos: {ex.Message}");
            }
        }

    }
}

