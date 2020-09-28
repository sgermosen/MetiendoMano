using League.Models;
using League.Repositories;
using League.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace League.Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        [Test]
        public void GetAllTeams_calls_repo_GetAllTeams_function_once()
        {
            var mockTeamRepo = new Mock<ITeamRepository>();
            mockTeamRepo.Setup(m => m.GetAllTeams()).Returns(new List<Team>());

            var mockGameRepo = new Mock<IGameRepository>();
            mockGameRepo.Setup(m => m.GetGamesByTeamId(It.IsAny<int>())).Returns(new List<Game>());

            var teamService = new TeamService(mockTeamRepo.Object, mockGameRepo.Object);

            teamService.GetAllTeams();

            mockTeamRepo.Verify(m => m.GetAllTeams(), Times.Once());
        }

        [Test]
        public void GetAllTeams_returns_teams_with_calculated_records()
        {
            var mockTeamRepo = new Mock<ITeamRepository>();
            mockTeamRepo.Setup(m => m.GetAllTeams()).Returns(new List<Team>() { new Team { Id = 0 } });

            var mockGameRepo = new Mock<IGameRepository>();
            mockGameRepo.Setup(m => m.GetGamesByTeamId(It.IsAny<int>())).Returns(new List<Game>() { new Game { HomeTeam = 0, AwayTeam = 1, HomeScore = 1, AwayScore = 0 } });

            var teamService = new TeamService(mockTeamRepo.Object, mockGameRepo.Object);

            var result = teamService.GetAllTeams();

            mockTeamRepo.Verify(m => m.GetAllTeams(), Times.Once());
            mockGameRepo.Verify(m => m.GetGamesByTeamId(0), Times.Once());

            Assert.IsTrue(result[0].Record == "1-0");
        }

        [Test]
        public void Reset_calls_repo_Reset_function_once()
        {
            var mockTeamRepo = new Mock<ITeamRepository>();
            var mockGameRepo = new Mock<IGameRepository>();

            var teamService = new TeamService(mockTeamRepo.Object, mockGameRepo.Object);

            teamService.Reset();

            mockTeamRepo.Verify(m => m.Reset(), Times.Once());
            mockGameRepo.Verify(m => m.Reset(), Times.Once());
        }
    }
}
