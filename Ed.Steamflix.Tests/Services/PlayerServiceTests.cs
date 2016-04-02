using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks.Repositories;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Ed.Steamflix.Tests.Services
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
            var model = service.GetRecentlyPlayedGamesAsync(_steamId).Result;

            Assert.AreNotEqual(default(RecentlyPlayedGames), model, "Games object is empty.");
            Assert.IsTrue(model.Count > 0, "Expected at least one recently played game.");
        }

        [TestMethod]
        public void GetRecentlyPlayedGamesMockSuccess()
        {
            var apiRepository = new TestApiRepository();
            var service = new PlayerService(apiRepository);
            var model = service.GetRecentlyPlayedGamesAsync(_steamId).Result;

            Assert.AreNotEqual(default(RecentlyPlayedGames), model, "Games object is empty.");
            Assert.IsTrue(model.Count > 0, "Expected at least one recently played game.");
            Assert.AreEqual(15, model.Count, "Incorrect amount of game items deserialized.");
            Assert.AreEqual(107410, model[0].AppId, "AppId incorrectly deserialized for the first item.");
        }

        [TestMethod]
        public void GetOwnedGamesSuccess()
        {
            var apiRepository = new ApiRepository();
            var service = new PlayerService(apiRepository);
            var model = service.GetOwnedGamesAsync(_steamId).Result;

            Assert.AreNotEqual(default(OwnedGames), model, "Games object is empty.");
            Assert.IsTrue(model.Count > 0, "Expected at least one recently played game.");
        }

        [TestMethod]
        public void GetOwnedGamesMockSuccess()
        {
            var apiRepository = new TestApiRepository();
            var service = new PlayerService(apiRepository);
            var model = service.GetOwnedGamesAsync(_steamId).Result;

            Assert.AreNotEqual(default(OwnedGames), model, "Games object is empty.");
            Assert.IsTrue(model.Count > 0, "Expected at least one recently played game.");
            Assert.AreEqual(199, model.Count, "Incorrect amount of game items deserialized.");
            Assert.AreEqual(212500, model[0].AppId, "AppId incorrectly deserialized for the first item.");
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
