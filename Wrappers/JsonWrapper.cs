﻿using System;
using System.IO;
using Newtonsoft.Json;

public class JsonWrapper
{
    public static string ConvertToJson(string jsonFilePath)
    {
        try
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            return jsonData;
        }
        catch (Exception ex) {
            Console.WriteLine($"Error al convertir el JSON a objetos: {ex.Message}");
            return null;
        }
        
    }

}
