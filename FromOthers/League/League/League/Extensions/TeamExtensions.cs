using League.DTOs;
using League.Models;

namespace League.Extensions
{
    public static class TeamExtensions
    {
        public static TeamDTO ConvertToDTO(this Team team)
        {
            TeamDTO teamDTO = new TeamDTO();

            teamDTO.Id = team.Id;
            teamDTO.Name = team.Name;

            return teamDTO;
        }
    }
}
