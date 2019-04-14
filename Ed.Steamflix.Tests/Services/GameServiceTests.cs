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

            _apiRepositoryMock.Setup(m => m.ApiCall(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string service, string method, string version, string parameters) => { return Task.FromResult(Resources.ResourceManager.GetString(method + "ResponseJson")); });

            _apiRepositoryMock.Setup(m => m.ReadUrl(It.IsAny<string>()))
                .Returns((string url) => { return Task.FromResult(Resources.ResourceManager.GetString(Regex.Replace(url, @"[\:\/\.\=\?]", ""))); });

            _communityRepositoryMock.Setup(m => m.GetStatsHtml())
                .Returns(Task.FromResult(Resources.ResourceManager.GetString("StatsHtml")));

            _targetReal = new GameService(_apiRepository, _communityRepository);
            _target = new GameService(_apiRepositoryMock.Object, _communityRepositoryMock.Object);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetRecentlyPlayedGames_Real_Success()
        {
            var model = await _targetReal.GetRecentlyPlayedGames(_steamId);

            Assert.NotEqual(default, model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
        }

        [Fact]
        public async Task GetRecentlyPlayedGames_Success()
        {
            var model = await _target.GetRecentlyPlayedGames(_steamId);

            Assert.NotEqual(default, model);
            Assert.True(model.Count > 0);
            Assert.Equal(15, model.Count);
            Assert.Equal(107410, model[0].AppId);
        }

        [Fact]
        public async Task GetRecentlyPlayedGames_NoSteamId_ReturnsNull()
        {
            var model = await _target.GetRecentlyPlayedGames(null);

            Assert.Null(model);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetOwnedGames_Real_Success()
        {
            var model = await _targetReal.GetOwnedGames(_steamId);

            Assert.NotEqual(default, model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
        }

        [Fact]
        public async Task GetOwnedGames_Success()
        {
            var model = await _target.GetOwnedGames(_steamId);

            Assert.NotEqual(default, model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
            Assert.Equal(199, model.Count);
            Assert.Equal(212500, model[0].AppId);
        }

        [Fact]
        public async Task GetOwnedGames_NoSteamId_ReturnsNull()
        {
            var model = await _target.GetOwnedGames(null);

            Assert.Null(model);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetGameInfo_Real_Success()
        {
            var game = await _targetReal.GetGameInfo(72850); // Skyrim

            Assert.True(game != null, "Expected a game.");
            Assert.Equal(72850, game.AppId);
        }

        [Fact]
        public async Task GetGameInfo_Success()
        {
            var game = await _target.GetGameInfo(72850); // Skyrim

            Assert.True(game != null, "Expected a game.");
            Assert.Equal(72850, game.AppId);
        }

        [Fact]
        public async Task GetGameInfo_NullApiResult_ReturnsNull()
        {
            _apiRepositoryMock.Setup(m => m.ReadUrl(It.IsAny<string>()))
                .Returns(Task.FromResult<string>(null));

            var game = await _target.GetGameInfo(999);

            Assert.True(game == null, "Expected no game.");
        }

        [Fact]
        public async Task GetGameInfo_GameNotFound_ReturnsNull()
        {
            _apiRepositoryMock.Setup(m => m.ReadUrl(It.IsAny<string>()))
                .Returns(Task.FromResult("{\"999\": {\"success\": false}}"));

            var game = await _target.GetGameInfo(999);

            Assert.True(game == null, "Expected no game.");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetPopular_Real_Success()
        {
            var games = await _targetReal.GetPopularGames();

            Assert.True(games.Count > 0, "Expected a different number of broadcasts.");
        }

        [Fact]
        public async Task GetPopularGames_Success()
        {
            var games = await _target.GetPopularGames();

            Assert.Equal(100, games.Count);
            Assert.Equal("Dota 2", games[0].Name);
            Assert.Equal(570, games[0].AppId);
        }
    }
}
