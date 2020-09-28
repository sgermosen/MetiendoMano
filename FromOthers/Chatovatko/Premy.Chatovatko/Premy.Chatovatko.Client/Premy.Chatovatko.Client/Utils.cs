using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Premy.Chatovatko.Client
{
    public class Utils
    {
        public static String GetConfigDirectory()
        {
            String path = FileSystem.Current.LocalStorage.Path + "/.chatovatko";
            Directory.CreateDirectory(path);
            return path;
        }

        public static String GetDatabaseAddress() => GetConfigDirectory() + "/client.db";
    }
}
