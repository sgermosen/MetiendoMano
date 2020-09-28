using System.Collections.Generic;
using League.Models;
using SQLite;

namespace League.Repositories
{
    public interface ITeamRepository
    {
        List<Team> GetAllTeams();
        void Initialize(SQLiteConnection conn);
        void Reset();
    }
}