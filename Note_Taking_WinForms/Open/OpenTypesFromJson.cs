using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Note_Taking_WinForms.Open
{
    static public class OpenTypesFromJson
    {
        static public  Dictionary<string, int> ReadFile(string fileName)
        {
            Dictionary<string, int> types = new Dictionary<string, int>();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                types = JsonSerializer.Deserialize<Dictionary<string, int>>(fs, options);
            }

            return types;
        }
    }
}
