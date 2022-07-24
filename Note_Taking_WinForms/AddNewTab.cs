using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Note_Taking_WinForms
{
    public partial class AddNewTab : Form
    {
        public AddNewTab()
        {
            InitializeComponent();
        }

        public string NewName { get; set; }

        private void AddButton_Click(object sender, EventArgs e)
        {
            NewName = NameTB.Text;

            if (NewName == "")
                MessageBox.Show("");
            else
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
