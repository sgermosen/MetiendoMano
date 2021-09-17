using System.Collections.Generic;
using ImpNotes.Interface;
using ImpNotes.Models;
using SQLite;
using Xamarin.Forms;

namespace ImpNotes.Services
{
    public class NoteService
    {
        SQLiteConnection Connection;

        public NoteService()
        {
            Connection = DependencyService.Get<IPsSqlite>().GetConnection();
            Connection.CreateTable<NotesModel>();
        }

        public bool Add(NotesModel notes)
        {
            int result = Connection.Insert(notes);
            if (result == 1) return true;

            return false;
        }

        public bool Update(NotesModel notes)
        {
            int result = Connection.Update(notes);
            if (result == 1) return true;

            return false;
        }

        public bool Delete(int id)
        {
            int result = Connection.Delete<NotesModel>(id);
            if (result == 1) return true;

            return false;
        }

        public List<NotesModel> GetNotes()
        {
            return Connection.Table<NotesModel>().ToList();
        }
    }
}
