using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks;
using Moq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Ed.Steamflix.Tests.Services
{
    /// <summary>
    /// Tests PlayerService method implementations.
    /// </summary>
    public class GameServiceTests
    {
        private readonly string _steamId = "76561197974081377"; // Me
        private readonly IApiRepository _apiRepository;
        private readonly ICommunityRepository _communityRepository;
        private readonly Mock<IApiRepository> _apiRepositoryMock;
        private readonly Mock<ICommunityRepository> _communityRepositoryMock;
        private readonly GameService _target;
        private readonly GameService _targetReal;

        public GameServiceTests()
        {
            _apiRepository = new ApiRepository();
            _communityRepository = new CommunityRepository();
            _apiRepositoryMock = new Mock<IApiRepository>();
            _communityRepositoryMock = new Mock<ICommunityRepository>();

            _apiRepositoryMock.Setup(m => m.ApiCallAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string service, string method, string version, string parameters) => { return Task.FromResult(Resources.ResourceManager.GetString(method + "ResponseJson")); });

            _apiRepositoryMock.Setup(m => m.ReadUrlAsync(It.IsAny<string>()))
                .Returns((string url) => { return Task.FromResult(Resources.ResourceManager.GetString(Regex.Replace(url, @"[\:\/\.\=\?]", ""))); });

            _communityRepositoryMock.Setup(m => m.GetStatsHtmlAsync())
                .Returns(Task.FromResult(Resources.ResourceManager.GetString("StatsHtml")));

            _targetReal = new GameService(_apiRepository, _communityRepository);
            _target = new GameService(_apiRepositoryMock.Object, _communityRepositoryMock.Object);
        }

        [Fact]
        public void GetRecentlyPlayedGames_Real_Success()
        {
            var model = _targetReal.GetRecentlyPlayedGamesAsync(_steamId).Result;

            Assert.NotEqual(default(List<Game>), model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
        }

        [Fact]
        public void GetRecentlyPlayedGames_Success()
        {
            var model = _target.GetRecentlyPlayedGamesAsync(_steamId).Result;

            Assert.NotEqual(default(List<Game>), model);
            Assert.True(model.Count > 0);
            Assert.Equal(15, model.Count);
            Assert.Equal(107410, model[0].AppId);
        }

        [Fact]
        public void GetRecentlyPlayedGames_NoSteamId_ReturnsNull()
        {
            var model = _target.GetRecentlyPlayedGamesAsync(null).Result;

            Assert.Equal(null, model);
        }

        [Fact]
        public void GetOwnedGames_Real_Success()
        {
            var model = _targetReal.GetOwnedGamesAsync(_steamId).Result;

            Assert.NotEqual(default(List<Game>), model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
        }

        [Fact]
        public void GetOwnedGames_Success()
        {
            var model = _target.GetOwnedGamesAsync(_steamId).Result;

            Assert.NotEqual(default(List<Game>), model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
            Assert.Equal(199, model.Count);
            Assert.Equal(212500, model[0].AppId);
        }

        [Fact]
        public void GetOwnedGames_NoSteamId_ReturnsNull()
        {
            var model = _target.GetOwnedGamesAsync(null).Result;

            Assert.Equal(null, model);
        }

        [Fact]
        public void GetGameInfo_Real_Success()
        {
            var game = _targetReal.GetGameInfoAsync(72850).Result; // Skyrim

            Assert.True(game != null, "Expected a game.");
            Assert.Equal(72850, game.AppId);
        }

        [Fact]
        public void GetGameInfo_Success()
        {
            var game = _target.GetGameInfoAsync(72850).Result; // Skyrim

            Assert.True(game != null, "Expected a game.");
            Assert.Equal(72850, game.AppId);
        }

        [Fact]
        public void GetGameInfo_NullApiResult_ReturnsNull()
        {
            _apiRepositoryMock.Setup(m => m.ReadUrlAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<string>(null));

            var game = _target.GetGameInfoAsync(999).Result;

            Assert.True(game == null, "Expected no game.");
        }

        [Fact]
        public void GetGameInfo_GameNotFound_ReturnsNull()
        {
            _apiRepositoryMock.Setup(m => m.ReadUrlAsync(It.IsAny<string>()))
                .Returns(Task.FromResult("{\"999\": {\"success\": false}}"));

            var game = _target.GetGameInfoAsync(999).Result;

            Assert.True(game == null, "Expected no game.");
        }

        [Fact]
        public void GetPopular_Real_Success()
        {
            var games = _targetReal.GetPopularGamesAsync().Result;

            Assert.True(games.Count > 0, "Expected a different number of broadcasts.");
        }

        [Fact]
        public void GetPopularGames_Success()
        {
            var games = _target.GetPopularGamesAsync().Result;

            Assert.Equal(100, games.Count);
            Assert.Equal("Dota 2", games[0].Name);
            Assert.Equal(570, games[0].AppId);
        }
    }
}
