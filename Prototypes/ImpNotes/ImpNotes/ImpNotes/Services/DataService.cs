using System.Collections.Generic;
using ImpNotes.Interface;
using ImpNotes.Models;
using SQLite;
using Xamarin.Forms;
using SQLiteNetExtensions.Extensions;
using System;
using System.IO;
using System.Linq;

namespace ImpNotes.Services
{
    public class DataService : IDisposable
    {
        SQLiteConnection Connection;

        public DataService()
        {
            Connection = DependencyService.Get<IPsSqlite>().GetConnection();
            Connection.CreateTable<NotesModel>();
            Connection.CreateTable<Concept>();
        }

        public bool Add<T>(T model)
        {
            int result = Connection.Insert(model);
            if (result == 1) return true;

            return false;
        }

        //public bool Add(T notes)
        //{
        //    int result = Connection.Insert(notes);
        //    if (result == 1) return true;

        //    return false;
        //}

        public bool Update<T>(T model)
        {
            int result = Connection.Update(model);
            if (result == 1) return true;

            return false;
        }

        //public bool Update(NotesModel notes)
        //{
        //    int result = Connection.Update(notes);
        //    if (result == 1) return true;

        //    return false;
        //}

        public bool Delete<T>(T model)
        {
            //int result = Connection.Delete<NotesModel>(id);
            int result = Connection.Delete(model);
            if (result == 1) return true;

            return false;
        }
        //public bool Delete<T>(T model)
        //{
        //    return Connection.Delete(model);
        //    this.connection.Delete(model);
        //}

        public T First<T>(bool WithChildren) where T : new()
        {
            if (WithChildren)
            {
                //return Connection.Table<T>().FirstOrDefault();
                //return Connection.GetAllWithChildren()
                return Connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return Connection.Table<T>().FirstOrDefault();
            }
        }



        public T Find<T>(int pk, bool WithChildren) where T : new()
        {
            if (WithChildren)
            {
               
                return Connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return Connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        // public List<NotesModel> GetNotes() 
        public List<T> GetList<T>(bool WithChildren) where T : new()
        {
            if (WithChildren)
            {
                return Connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return Connection.Table<T>().ToList();
            }


            // return Connection.Table<NotesModel>().ToList();
        }
        //public List<T> GetNotes <T>() where T : class
        //{
        //    return Connection.Table<T>().ToList();
        //}

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
