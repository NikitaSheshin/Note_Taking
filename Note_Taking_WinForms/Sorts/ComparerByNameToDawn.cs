using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_Taking_WinForms.Sorts
{
    public class ComparerByNameToDawn : IComparer<Note>
    {
        public int Compare(Note x, Note y)
           => string.Compare(y.Name, x.Name);
    }
}
