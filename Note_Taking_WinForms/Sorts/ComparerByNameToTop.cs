
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_Taking_WinForms.Sorts
{
    public class ComparerByNameToTop : IComparer<Note>
    {
        public int Compare(Note x, Note y)
           => string.Compare(x.Name, y.Name);
    }
}
