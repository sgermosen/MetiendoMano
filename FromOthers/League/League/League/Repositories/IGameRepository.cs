using League.Models;
using System.Collections.Generic;
using SQLite;

namespace League.Repositories
{
    public interface IGameRepository
    {
        List<Game> GetGamesByTeamId(int teamId);
        void Initialize(SQLiteConnection connection);
        void Reset();
    }
}
