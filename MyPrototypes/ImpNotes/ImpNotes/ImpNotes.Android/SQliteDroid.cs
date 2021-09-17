using System.IO;
using ImpNotes.Droid;
using ImpNotes.Interface;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQliteDroid))]
namespace ImpNotes.Droid
{
    public class SQliteDroid : IPsSqlite
    {
        public SQLiteConnection GetConnection()
        {
            var dbase = "impNotesDb";
            var dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dbpath, dbase);
            var connection = new SQLiteConnection(path);
            return connection;

        }
    }
}