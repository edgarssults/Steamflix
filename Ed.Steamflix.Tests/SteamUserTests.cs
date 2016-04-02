using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;

namespace Ed.Steamflix.Tests
{
    /// <summary>
    /// Tests SteamUser method implementations.
    /// </summary>
    [TestClass]
    public class SteamUserTests
    {
        private readonly string _steamId = "76561197974081377"; // Me
        private readonly string _vanityUrl = "edgarssults";

        [TestMethod]
        public void GetFriendListSuccess()
        {
            var apiRepository = new ApiRepository();
            var service = new SteamUser(apiRepository);
            var friends = service.GetFriendListAsync(_steamId).Result;

            Assert.IsTrue(friends.Friends.Count > 0, "Expected at least one friend.");
        }

        [TestMethod]
        public void GetPlayerSummariesSuccess()
        {
            var apiRepository = new ApiRepository();
            var service = new SteamUser(apiRepository);
            var playerSummaries = service.GetPlayerSummariesAsync(new List<string> { _steamId }).Result;

            Assert.IsTrue(playerSummaries.Players.Count > 0, "Expected at least one player.");
        }

        [TestMethod]
        public void GetFriendListMockSuccess()
        {
            var apiRepository = new TestApiRepository();
            var service = new SteamUser(apiRepository);
            var friends = service.GetFriendListAsync(_steamId).Result;

            Assert.IsTrue(friends.Friends.Count > 0, "Expected at least one friend.");
        }

        [TestMethod]
        public void GetPlayerSummariesMockSuccess()
        {
            var apiRepository = new TestApiRepository();
            var service = new SteamUser(apiRepository);
            var playerSummaries = service.GetPlayerSummariesAsync(new List<string> { _steamId }).Result;

            Assert.IsTrue(playerSummaries.Players.Count > 0, "Expected at least one player.");
        }

        [TestMethod]
        public void ResolveVanityUrlSuccess()
        {
            var apiRepository = new ApiRepository();
            var service = new SteamUser(apiRepository);
            var userData = service.ResolveVanityUrl(_vanityUrl).Result;

            Assert.IsTrue(userData.Success == 1, "Expected a successful response.");
        }

        [TestMethod]
        public void ResolveVanityUrlMockSuccess()
        {
            var apiRepository = new TestApiRepository();
            var service = new SteamUser(apiRepository);
            var userData = service.ResolveVanityUrl(_vanityUrl).Result;

            Assert.IsTrue(userData.Success == 1, "Expected a successful response.");
        }
    }
}
