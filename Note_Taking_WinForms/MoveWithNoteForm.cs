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
    public partial class MoveWithNoteForm : Form
    {
        public Note newNote;

        public MoveWithNoteForm(Note note, MainForm.ActionType mode)
        {
            InitializeComponent();

            newNote = note;
            NameTextBox.Text = note.Name;
            ContentTextBox.Text = note.Text;
            TypeBox.Text = note.Type;

            if (mode == MainForm.ActionType.Show)
            {
                NameTextBox.Enabled = false;
                ContentTextBox.Enabled = false;
                TypeBox.Enabled = false;

                OkButton.Text = "Вернуться";
            }
            else if (mode == MainForm.ActionType.Add)
                OkButton.Text = "Добавить";
            else if (mode == MainForm.ActionType.Change)
                OkButton.Text = "Изменить";
        }

        private void ExitClick(object sender, EventArgs e)
        {
            newNote.Text = ContentTextBox.Text;

            
            if (NameTextBox.Text == "")
                newNote.Name = "Без названия";
            else
                newNote.Name = NameTextBox.Text;

            if (TypeBox.Text == "")
                newNote.Type = "Без темы";
            else
                newNote.Type = TypeBox.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
