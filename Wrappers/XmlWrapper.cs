using System;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

namespace practiquesIEI.Wrappers
{
    public class XmlWrapper
    {

        public static string ConvertToJson(string xmlFilePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            string json = JsonConvert.SerializeXmlNode(xmlDoc);
            return json;
        }
    }
}

