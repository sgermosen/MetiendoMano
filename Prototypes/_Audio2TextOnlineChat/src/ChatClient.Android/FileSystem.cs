using ChatClient.Services;
using System;
using System.IO;

namespace ChatClient.Droid
{
    class FileSystem : IFileSystem
    {
        public string LocalDir => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public string AppDataDir => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public string AudioDir => Path.Combine(AppDataDir, "data/audio");
        public string TempDir => Path.Combine(AppDataDir, "temp/");
    }
}
