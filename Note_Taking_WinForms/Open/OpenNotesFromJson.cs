using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Note_Taking_WinForms.Open
{
    static public class OpenNotesFromJson
    {
        static public List<Note> ReadFile(string fileName)
        {
            List<Note> notes = new List<Note>();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                notes = JsonSerializer.Deserialize<List<Note>>(fs, options);
                if (notes == null)
                    notes = new List<Note>();
            }

            return notes;
        }
    }
}
