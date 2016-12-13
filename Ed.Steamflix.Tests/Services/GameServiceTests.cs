using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks.Repositories;
using System.Collections.Generic;
using Xunit;

namespace Ed.Steamflix.Tests.Services
{
    /// <summary>
    /// Tests PlayerService method implementations.
    /// </summary>
    public class GameServiceTests
    {
        private readonly string _steamId = "76561197974081377"; // Me
        private IApiRepository _apiRepository;
        private IApiRepository _testApiRepository;
        private ICommunityRepository _communityRepository;
        private ICommunityRepository _testCommunityRepository;

        public GameServiceTests()
        {
            _apiRepository = new ApiRepository();
            _testApiRepository = new TestApiRepository();
            _communityRepository = new CommunityRepository();
            _testCommunityRepository = new TestCommunityRepository();
        }

        [Fact]
        public void GetRecentlyPlayedGamesSuccess()
        {
            var service = new GameService(_apiRepository, _communityRepository);
            var model = service.GetRecentlyPlayedGamesAsync(_steamId).Result;

            Assert.NotEqual(default(List<Game>), model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
        }

        [Fact]
        public void GetRecentlyPlayedGamesMockSuccess()
        {
            var service = new GameService(_testApiRepository, _testCommunityRepository);
            var model = service.GetRecentlyPlayedGamesAsync(_steamId).Result;

            Assert.NotEqual(default(List<Game>), model);
            Assert.True(model.Count > 0);
            Assert.Equal(15, model.Count);
            Assert.Equal(107410, model[0].AppId);
        }

        [Fact]
        public void GetOwnedGamesSuccess()
        {
            var service = new GameService(_apiRepository, _communityRepository);
            var model = service.GetOwnedGamesAsync(_steamId).Result;

            Assert.NotEqual(default(List<Game>), model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
        }

        [Fact]
        public void GetOwnedGamesMockSuccess()
        {
            var service = new GameService(_testApiRepository, _testCommunityRepository);
            var model = service.GetOwnedGamesAsync(_steamId).Result;

            Assert.NotEqual(default(List<Game>), model);
            Assert.True(model.Count > 0, "Expected at least one recently played game.");
            Assert.Equal(199, model.Count);
            Assert.Equal(212500, model[0].AppId);
        }

        [Fact]
        public void GetGameInfoSuccess()
        {
            var service = new GameService(_apiRepository, _communityRepository);
            var game = service.GetGameInfoAsync(72850).Result; // Skyrim

            Assert.True(game != null, "Expected a game.");
            Assert.Equal(72850, game.AppId);
        }

        [Fact]
        public void GetGameInfoMockSuccess()
        {
            var service = new GameService(_testApiRepository, _testCommunityRepository);
            var game = service.GetGameInfoAsync(72850).Result; // Skyrim

            Assert.True(game != null, "Expected a game.");
            Assert.Equal(72850, game.AppId);
        }

        [Fact]
        public void GetPopularGamesSuccess()
        {
            var service = new GameService(_apiRepository, _communityRepository);
            var games = service.GetPopularGamesAsync().Result;

            Assert.True(games.Count > 0, "Expected a different number of broadcasts.");
        }

        [Fact]
        public void GetPopularGamesMockSuccess()
        {
            var service = new GameService(_testApiRepository, _testCommunityRepository);
            var games = service.GetPopularGamesAsync().Result;

            Assert.Equal(100, games.Count);
            Assert.Equal("Dota 2", games[0].Name);
            Assert.Equal(570, games[0].AppId);
        }
    }
}
