using League.Models;
using System;

namespace League.ViewModels
{
    public class TeamViewModel
    {
        private Team _team;

        public TeamViewModel(Team team)
        {
            _team = team;
        }

        public String Name
        {
            get
            {
                return _team.Name;
            }
        }

        public String Record
        {
            get
            {
                return "0-0";
            }
        }
    }
}
