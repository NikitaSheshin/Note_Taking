using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_Taking_WinForms
{
    [Serializable]
    public class Note
    {
        string type;
        string name;
        string text;

        public enum SortState
        {
            NameAsc,    // по имени по возрастанию
            NameDesc   // по имени по убыванию
        }

        public int Id { get; set; }

        public Note()
        {
            this.name = "";
            this.text = "";
            this.type = "";
        }

        public string Type { get {  return type; } set { type = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Text { get { return text; } set { text = value; } }
    }
}
