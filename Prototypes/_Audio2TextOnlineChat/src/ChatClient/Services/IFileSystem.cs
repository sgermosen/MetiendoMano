namespace ChatClient.Services
{
    public interface IFileSystem
    {
        string LocalDir { get; }
        string AppDataDir { get; }
        string AudioDir { get; }
        string TempDir { get; }
    }
}
