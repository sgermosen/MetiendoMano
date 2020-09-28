using League.Models;
using League.Repositories;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

[assembly: Dependency(typeof(ITeamRepository))]
namespace League.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private SQLiteConnection Connection;

        public TeamRepository()
        { }

        public void Initialize(SQLiteConnection conn)
        {
            Connection = conn;

            Connection.CreateTable<Team>();

            var allTeams = Connection.Table<Team>().ToList();
            if (allTeams.Count == 0)
            {
                CreateInitializationData();
            }
        }

        public void Reset()
        {
            Connection.DeleteAll<Team>();
            CreateInitializationData();
        }

        public List<Team> GetAllTeams()
        {
            return Connection.Table<Team>().ToList();
        }

        private void CreateInitializationData()
        {
            var team1 = new Team() { Id = 0, Name = "Steelers" };
            var team2 = new Team() { Id = 1, Name = "Ravens" };

            var teamList = new List<Team> { team1, team2 };

            Connection.InsertAll(teamList);
        }
    }
}
