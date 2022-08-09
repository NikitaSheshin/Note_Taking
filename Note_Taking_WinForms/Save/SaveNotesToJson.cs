using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Note_Taking_WinForms.Save
{
    static public class SaveNotesToJson
    {
        static public void WriteToFile(string fileName, List<Note> notes)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };

            string json = JsonSerializer.Serialize(notes, options);
            File.WriteAllText(fileName, json);
        }
    }
}
