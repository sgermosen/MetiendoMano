using League.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace League.Repositories
{
    public class GameRepository : IGameRepository
    {
        private SQLiteConnection Connection;

        public GameRepository(SQLiteConnection conn)
        { }

        public void Initialize(SQLiteConnection conn)
        {
            Connection = conn;

            Connection.CreateTable<Game>();

            var allGames = Connection.Table<Game>().ToList();
            if (allGames.Count == 0)
            {
                CreateInitializationData();
            }
        }

        public List<Game> GetGamesByTeamId(int teamId)
        {
            return Connection.Table<Game>().Where(g => g.AwayTeam == teamId || g.HomeTeam == teamId).ToList();
        }

        public void Reset()
        {
            Connection.DeleteAll<Game>();
            CreateInitializationData();
        }

        private void CreateInitializationData()
        {
            var game1 = new Game { Id = 0, HomeTeam = 17, AwayTeam = 18, HomeScore = 21, AwayScore = 7, Completed = true, Week = 1};

            var gameList = new List<Game> { game1 };

            Connection.InsertAll(gameList);
        }
    }
}
