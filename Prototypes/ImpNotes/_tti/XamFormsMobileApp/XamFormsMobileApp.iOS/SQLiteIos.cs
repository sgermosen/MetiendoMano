using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;
using XamFormsMobileApp.iOS;
using XamFormsMobileApp.ISQlite;

[assembly: Dependency(typeof(SQLiteIos))]
namespace XamFormsMobileApp.iOS
{
    public class SQLiteIos : Isqlite
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "Database.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}