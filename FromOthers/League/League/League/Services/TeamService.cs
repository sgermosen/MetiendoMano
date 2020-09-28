using League.DTOs;
using League.Extensions;
using League.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;
using League.Models;

namespace League.Services
{
    public class TeamService : ITeamService
    {
        private ITeamRepository TeamRepo;
        private IGameRepository GameRepo;

        public TeamService(ITeamRepository teamRepo, IGameRepository gameRepo)
        {
            TeamRepo = teamRepo;
            GameRepo = gameRepo;
        }

        public List<TeamDTO> GetAllTeams()
        {
            var teams = TeamRepo.GetAllTeams().Select((team) => team.ConvertToDTO()).ToList<TeamDTO>();
            PopulateRecords(teams);
            return teams;
        }

        private void PopulateRecords(List<TeamDTO> teams)
        {
            foreach (TeamDTO team in teams)
            {
                var allGames = GameRepo.GetGamesByTeamId(team.Id);

                var wins = allGames.Where<Game>(g =>
                                                (g.HomeTeam == team.Id && g.HomeScore > g.AwayScore) ||
                                                (g.AwayTeam == team.Id && g.AwayScore > g.HomeScore)).Count();
                var losses = allGames.Count - wins;

                team.Record = String.Format("{0}-{1}", wins, losses);
            }
        }

        public void Reset()
        {
            TeamRepo.Reset();
            GameRepo.Reset();
        }
    }
}
