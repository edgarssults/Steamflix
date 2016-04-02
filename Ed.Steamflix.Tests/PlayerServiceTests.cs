using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Ed.Steamflix.Tests
{
    /// <summary>
    /// Tests PlayerService method implementations.
    /// </summary>
    [TestClass]
    public class PlayerServiceTests
    {
        private readonly string _steamId = "76561197974081377"; // Me

        [TestMethod]
        public void GetRecentlyPlayedGamesSuccess()
        {
            var apiRepository = new ApiRepository();
            var service = new PlayerService(apiRepository);
            var games = service.GetRecentlyPlayedGamesAsync(_steamId).Result;

            Assert.IsTrue(games.TotalCount > 0, "Expected at least one recently played game.");
            Assert.IsTrue(games.Games.Count > 0, "Expected at least one recently played game.");
        }

        [TestMethod]
        public void GetOwnedGamesSuccess()
        {
            var apiRepository = new ApiRepository();
            var service = new PlayerService(apiRepository);
            var games = service.GetOwnedGamesAsync(_steamId).Result;

            Assert.IsTrue(games.GameCount > 0, "Expected at least one recently played game.");
            Assert.IsTrue(games.Games.Count > 0, "Expected at least one recently played game.");
        }

        [TestMethod]
        public void GetRecentlyPlayedGamesMockSuccess()
        {
            var apiRepository = new TestApiRepository();
            var service = new PlayerService(apiRepository);
            var games = service.GetRecentlyPlayedGamesAsync(_steamId).Result;

            Assert.IsTrue(games.TotalCount > 0, "Expected at least one recently played game.");
            Assert.IsTrue(games.Games.Count > 0, "Expected at least one recently played game.");
        }

        [TestMethod]
        public void GetOwnedGamesMockSuccess()
        {
            var apiRepository = new TestApiRepository();
            var service = new PlayerService(apiRepository);
            var games = service.GetOwnedGamesAsync(_steamId).Result;

            Assert.IsTrue(games.GameCount > 0, "Expected at least one recently played game.");
            Assert.IsTrue(games.Games.Count > 0, "Expected at least one recently played game.");
        }

        [TestMethod]
        public void GetGameInfoSuccess()
        {
            var apiRepository = new ApiRepository();
            var service = new PlayerService(apiRepository);
            var game = service.GetGameInfoAsync(_steamId, 72850).Result; // Skyrim

            Assert.IsTrue(game != null, "Expected a game.");
            Assert.AreEqual(72850, game.AppId, "Expected the same app identifier.");
        }

        [TestMethod]
        public void GetGameInfoMockSuccess()
        {
            var apiRepository = new ApiRepository();
            var service = new PlayerService(apiRepository);
            var game = service.GetGameInfoAsync(_steamId, 72850).Result; // Skyrim

            Assert.IsTrue(game != null, "Expected a game.");
            Assert.AreEqual(72850, game.AppId, "Expected the same app identifier.");
        }
    }
}
