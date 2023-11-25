using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using practiquesIEI.Entities;

namespace practiquesIEI.Extractors
{
    public class CVextractor
    {
        public static void LoadJsonDataIntoDatabase(string jsonData)
        {
            
            // Deserializar JSON a una lista de objetos dinámicos
            List<dynamic> dynamicDataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            foreach (var dynamicData in dynamicDataList)
            {
                centro_educativo centro = JsonACentro(dynamicData);
                provincia provincia = new provincia();
                string codProvS = centro.cod_postal.ToString();
                provincia.codigo = int.Parse(codProvS.Substring(0, 2));
                provincia.nombre = dynamicData.PROVINCIA;
            }
        }
        static centro_educativo JsonACentro(dynamic dynamicData) {
            centro_educativo centro = new centro_educativo();
            // Extraer propiedades específicas y construir las columnas que deseas
            string tipoVia = dynamicData.TIPO_VIA;
            string direccion = dynamicData.DIRECCION;
            string numero = dynamicData.NUMERO;
            // Concatenar los valores en una sola cadena para la columna "Direccion"
            centro.direccion = $"{tipoVia} {direccion} {numero}";
            centro.nombre = dynamicData.DENOMINACION;
            // Extraer el código postal para la columna "CodigoPostal"
            centro.cod_postal = int.Parse(dynamicData.CODIGO_POSTAL);
            centro.telefono = dynamicData.TELEFONO;
            centro.descripcion = dynamicData.URL_VA;
            switch (dynamicData.REGIMEN)
            {
                case "PUB": centro.tipo = tipo_centro.Público; break;
                case "PRIV": centro.tipo = tipo_centro.Privado; break;
                case "PRIV.CONC": centro.tipo = tipo_centro.Concertado; break;
                case "OTROS": centro.tipo = tipo_centro.Otros; break;
            }
            return centro;
        }

    }
}

