﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Note_Taking_WinForms
{
    public class Data
    {
        List<Note> allNotes;

        public List<Note> AllNotes { get { return allNotes; } set { allNotes = value; } }

        public Data(string fileName)
        {
            allNotes = Open.OpenNotesFromJson.ReadFile(fileName);
        }

        public bool AddNote(Note note)
        {
            foreach (Note el in allNotes)
                if (el.Name == note.Name)
                    return false;

            allNotes.Add(note);
            return true;
        }

        public void ChangeNote(Note note, string oldName)
        {
            for (int i = 0; i < allNotes.Count; i++)
                if (allNotes[i].Name == oldName)
                    allNotes[i] = note;
        }

        public void DeleteNote(string name)
        {
            allNotes.Remove(GetNoteByName(name));
        }

        public void Sort(string type)
        {
            if (type == "NameTop")
                allNotes.Sort(new Sorts.ComparerByNameToTop());
            else if (type == "NameDawn")
                allNotes.Sort(new Sorts.ComparerByNameToDawn());
        }

        public Note GetNoteByName(string name)
        {
            foreach(Note note in allNotes)
                if(note.Name == name)
                    return note;
            return null;
        }

        public void SaveChanges(string fileName)
        {
            Save.SaveNotesToJson.WriteToFile(fileName, AllNotes);
        }
    }
}
