using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

public class CsvWrapper
{
    public static string ConvertToJson(string csvFilePath)
    {
        var records = new List<Dictionary<string, object>>();

        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var recordsFromCsv = csv.GetRecords<dynamic>();

            foreach (var record in recordsFromCsv)
            {
                var dictionary = new Dictionary<string, object>();
                foreach (var property in record.GetType().GetProperties())
                {
                    dictionary[property.Name] = property.GetValue(record);
                }
                records.Add(dictionary);
            }
        }

        string json = JsonConvert.SerializeObject(records);
        return json;
    }
}


