using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Note_Taking_WinForms
{
    public partial class MainForm : Form
    {
        const string FileNameForNotes = "State.json";
        const string FileNameForTypes = "Types.json";
 
        public enum ActionType { Add, Show, Change };

        Dictionary<string, string> weekDays = new Dictionary<string, string>()
        {
            { "Sunday", "ВС" },
            { "Monday", "ПН" },
            { "Tuesday", "ВТ" },
            { "Wednesday", "СР" },
            { "Thursday", "ЧТ" },
            { "Friday", "ПТ" },
            { "Saturday", "СБ" },
        };

        Dictionary<string, int> types = new Dictionary<string, int>()
        {
            { "Все", 1 },
            { "Без темы", 2 },
            { "Работа", 3 },
            { "Дом", 4 },
            { "Покупки", 5 }
        };

        Data data;

        public MainForm()
        {
            InitializeComponent();
            DateTime data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeNow.Text = weekDays[data.DayOfWeek.ToString()] + ", " + DateTime.Now.ToLongDateString();
        }

        private ListBox GetCurrentListBox(int index)
            => (ListBox)Controls.Find("listBox" + index, true)[0];


        private void MoveWithNote(object sender, EventArgs e)
        {
            MoveWithNoteForm moveForm;
            ListBox listBox = GetCurrentListBox(Fields.SelectedIndex + 1);

            if ((sender as Button).Text == "Добавить")
            {
                moveForm = new MoveWithNoteForm(new Note(), ActionType.Add, types.Keys.ToList());

                if (moveForm.ShowDialog() == DialogResult.OK)
                {
                    if (data.AddNote(moveForm.newNote))
                        ShowNotes(types[Fields.SelectedTab.Text]);
                    else
                        MessageBox.Show("Запись с таким названием уже существует");
                }
            }
            else if ((sender as Button).Text == "Просмотр" && listBox.SelectedItem != null)
            {
                moveForm = new MoveWithNoteForm(data.GetNoteByName(listBox.SelectedItem.ToString()), ActionType.Show
                    , types.Keys.ToList());
                moveForm.ShowDialog();
            }   
            else if ((sender as Button).Text == "Изменить")
            {
                moveForm = new MoveWithNoteForm(data.GetNoteByName(listBox.SelectedItem.ToString()), ActionType.Change,
                    types.Keys.ToList());

                if (moveForm.ShowDialog() == DialogResult.OK)
                {
                    data.ChangeNote(moveForm.newNote, listBox.SelectedItem.ToString());
                    ShowNotes(types[Fields.SelectedTab.Text]);
                }
            }
        }

        private void NotesField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = GetCurrentListBox(Fields.SelectedIndex + 1);

            if (listBox.SelectedItem != null)
            {
                ChangeButton.Enabled = true;
                DeleteButton.Enabled = true;
                ShowButton.Enabled = true;
            }
            else
            {
                ChangeButton.Enabled = false;
                DeleteButton.Enabled = false;
                ShowButton.Enabled = false;
            }
        }

        private void Delete(object sender, EventArgs e)
        {
            ListBox listBox = GetCurrentListBox(Fields.SelectedIndex + 1);
            int selInd = listBox.SelectedIndex;

            data.DeleteNote(listBox.SelectedItem.ToString());
            ShowNotes(types[Fields.SelectedTab.Text]);

            if (selInd > 0)
                listBox.SelectedItem = listBox.Items[selInd - 1];
            else if (listBox.Items.Count > 0)
                listBox.SelectedItem = listBox.Items[0]; ;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            data = new Data(FileNameForNotes);
            ShowNotes(types[Fields.SelectedTab.Text]);
            AddTags();
        }

        private void AddTags()
        {
            types = Open.OpenTypesFromJson.ReadFile(FileNameForTypes);

            for (int i = 5; i < types.Count; i++)
                AddOneTab(types.Keys.ToList()[i], i + 1);
        }

        private void Sort(object sender, EventArgs e)
        {
            data.Sort((sender as ToolStripMenuItem).Name);
            ShowNotes(types[Fields.SelectedTab.Text]);
        }

        private void ShowNotes(int type)
        {
            ListBox listBox = GetCurrentListBox(Fields.SelectedIndex + 1);
            listBox.Items.Clear();
            listBox.Items.AddRange((from note in data.AllNotes
                                    where type == 1 || types[note.Type] == type
                                    select note.Name.ToString()).ToArray());
        }
        

        private void AddOneTab(string nameForNewTab, int index)
        {
            TabPage tabPage = new TabPage();
            tabPage.Text = "+";

            ListBox listBox = new ListBox();
            listBox.SelectedIndexChanged += NotesField_SelectedIndexChanged;
            listBox.Dock = DockStyle.Fill;
            listBox.Name = "listBox" + index;

            Fields.Controls[index - 1].Text = nameForNewTab;
            Fields.Controls[index - 1].Controls.Add(listBox);

            Fields.Controls.Add(tabPage);
        }

        private void Fields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as TabControl).SelectedTab.Text == "+")
            {
                AddNewTab addNewTab = new AddNewTab();

                if (addNewTab.ShowDialog() == DialogResult.OK)
                {
                    AddOneTab(addNewTab.NewName, Fields.SelectedIndex + 1);
                    types.Add(addNewTab.NewName, Fields.SelectedIndex + 1);
                }
                else
                    (sender as TabControl).SelectedIndex -= 1;
            }
            else
            {
                ShowNotes(types[Fields.SelectedTab.Text]);
                NotesField_SelectedIndexChanged(sender, e);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.SaveChanges(FileNameForNotes);
            Save.SaveTypesToJson.WriteToFile(FileNameForTypes, types);
        }
    }
}
