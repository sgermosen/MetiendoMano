using League.DTOs;
using System.Collections.Generic;

namespace League.ViewModels
{
    public class StandingsViewModel
    {
        private List<TeamDTO> Teams;

        public List<TeamDTO> Standings {
            get
            {
                return Teams;
            }
        }

        public StandingsViewModel()
        {
            var allTeams = App.TeamService.GetAllTeams();

            Teams = allTeams;
        }
    }
}
