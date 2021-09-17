using ChatClient.Services;
using System.IO;

namespace ChatClient.iOS
{
    class FileSystem : IFileSystem
    {
        public string LocalDir => Directory.GetCurrentDirectory();
        public string AppDataDir => Path.Combine(LocalDir, "data/");
        public string AudioDir => Path.Combine(LocalDir, "data/audio");
        public string TempDir => Path.Combine(LocalDir, "temp/");
    }
}
