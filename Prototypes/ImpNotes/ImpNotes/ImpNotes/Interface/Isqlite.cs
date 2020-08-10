using SQLite;

namespace ImpNotes.Interface
{
    public interface IPsSqlite
    {
        SQLiteConnection GetConnection();
    }
}
