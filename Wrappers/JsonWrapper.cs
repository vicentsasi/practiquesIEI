using System;
using System.IO;
using Newtonsoft.Json;

public class JsonWrapper
{
    public static string ConvertToJson(string jsonFilePath)
    {
        string jsonData = File.ReadAllText(jsonFilePath);
        return jsonData;
    }

}
