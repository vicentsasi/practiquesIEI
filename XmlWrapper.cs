using System;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

public class XmlWrapper
{

    public static string ConvertToJson(string xmlFilePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFilePath);

        string json = JsonConvert.SerializeXmlNode(xmlDoc);
        return json;
    }

    // Ejemplo de uso
    static void Main()
    {
        string xmlFilePath = "archivo.xml";
        string jsonData = ConvertToJson(xmlFilePath);
        Console.WriteLine(jsonData);
    }
}

