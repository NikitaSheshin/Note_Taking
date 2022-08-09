using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Note_Taking_WinForms.Save
{
    static public class SaveTypesToJson
    {
        static public void WriteToFile(string fileName, Dictionary<string, int> types)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };

            string json = JsonSerializer.Serialize(types, options);
            File.WriteAllText(fileName, json);
        }
    }
}
