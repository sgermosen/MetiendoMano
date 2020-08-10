using ChatClient.Services;
using System.IO;
using Windows.Storage;

namespace ChatClient.UWP
{
    class FileSystem : IFileSystem
    {
        public string LocalDir => Directory.GetCurrentDirectory();
        public string AppDataDir => ApplicationData.Current.LocalFolder.Path;
        public string AudioDir => Path.Combine(AppDataDir, "data/audio");
        public string TempDir => ApplicationData.Current.TemporaryFolder.Path;
    }
}
