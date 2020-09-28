using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Premy.Chatovatko.Client
{
    public static class Utils
    {
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
        public static String GetHomeDir()
        {
            string homePath = (!IsWindows())
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            return homePath;
        }

        public static String GetConfigDirectory()
        {
            String path = GetHomeDir() + "/.chatovatko";
            Directory.CreateDirectory(path);
            return path;
        }

        public static String GetDatabaseAddress() => GetConfigDirectory() + "/client.db";

    }
}
