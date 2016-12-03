using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks.Repositories;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;

namespace Ed.Steamflix.Tests.Services
{
    /// <summary>
    /// Tests SteamUser method implementations.
    /// </summary>
    [TestClass]
    public class UserServiceTests
    {
        private readonly string _steamId = "76561197974081377"; // Me
        private readonly string _vanityUrl = "edgarssults";
        private IApiRepository _apiRepository;
        private IApiRepository _testApiRepository;

        [TestInitialize]
        public void Initialize()
        {
            _apiRepository = new ApiRepository();
            _testApiRepository = new TestApiRepository();
        }

        [TestMethod]
        public void GetFriendListSuccess()
        {
            var service = new UserService(_apiRepository);
            var model = service.GetFriendListAsync(_steamId).Result;

            Assert.AreNotEqual(default(FriendsList), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Friends.Count > 0, "Expected at least one friend.");
        }

        [TestMethod]
        public void GetFriendListMockSuccess()
        {
            var service = new UserService(_testApiRepository);
            var model = service.GetFriendListAsync(_steamId).Result;

            Assert.AreNotEqual(default(FriendsList), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Friends.Count > 0, "Expected at least one friend.");
            Assert.AreEqual(34, model.Friends.Count, "Incorrect amount of friend items deserialized.");
            Assert.AreEqual("76561197961947067", model.Friends[0].SteamId, "SteamId incorrectly deserialized for the first item.");
        }

        [TestMethod]
        public void GetPlayerSummariesSuccess()
        {
            var service = new UserService(_apiRepository);
            var model = service.GetPlayerSummariesAsync(new List<string> { _steamId }).Result;

            Assert.AreNotEqual(default(PlayerSummaries), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Players.Count > 0, "Expected at least one player.");
        }

        [TestMethod]
        public void GetPlayerSummariesMockSuccess()
        {
            var service = new UserService(_testApiRepository);
            var model = service.GetPlayerSummariesAsync(new List<string> { _steamId }).Result;

            Assert.AreNotEqual(default(PlayerSummaries), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Players.Count > 0, "Expected at least one player.");
            Assert.AreEqual(2, model.Players.Count, "Incorrect amount of player items deserialized.");
            Assert.AreEqual("76561198084024217", model.Players[0].SteamId, "SteamId incorrectly deserialized for the first item.");
        }

        [TestMethod]
        public void ResolveVanityUrlSuccess()
        {
            var service = new UserService(_apiRepository);
            var model = service.ResolveVanityUrlAsync(_vanityUrl).Result;

            Assert.AreNotEqual(default(UserData), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Success == 1, "Expected a successful response.");
        }

        [TestMethod]
        public void ResolveVanityUrlMockSuccess()
        {
            var service = new UserService(_testApiRepository);
            var model = service.ResolveVanityUrlAsync(_vanityUrl).Result;

            Assert.AreNotEqual(default(UserData), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Success == 1, "Expected a successful response.");
            Assert.AreEqual(1, model.Success, "Success code incorrectly deserialized.");
            Assert.AreEqual("76561197974081377", model.SteamId, "SteamId incorrectly deserialized.");
        }
    }
}
