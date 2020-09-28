using System.Collections.Generic;
using League.DTOs;

namespace League.Services
{
    public interface ITeamService
    {
        List<TeamDTO> GetAllTeams();
        void Reset();
    }
}