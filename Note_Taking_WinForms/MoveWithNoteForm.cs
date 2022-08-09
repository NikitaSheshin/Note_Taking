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

        public MoveWithNoteForm(Note note, MainForm.ActionType mode, List<string> types)
        {
            InitializeComponent();

            AddTypes(types);

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

        private void AddTypes(List<string> types)
        {
            TypeBox.Items.Add("");
            TypeBox.Items.AddRange((from type in types
                                    where type != "Все" && type != "Без темы" 
                                    select type).ToArray());
        }

        private void ExitClick(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "")
                MessageBox.Show("Нельзя создать заметку без названия");
            else
            {
                newNote.Name = NameTextBox.Text;
                newNote.Text = ContentTextBox.Text;

                if (TypeBox.Text == "")
                    newNote.Type = "Без темы";
                else
                    newNote.Type = TypeBox.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
