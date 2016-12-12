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
        private readonly string _profileUrl = "http://steamcommunity.com/id/edgarssults";
        private IApiRepository _apiRepository;
        private IApiRepository _testApiRepository;
        private ICommunityRepository _communityRepository;
        private ICommunityRepository _testCommunityRepository;

        [TestInitialize]
        public void Initialize()
        {
            _apiRepository = new ApiRepository();
            _testApiRepository = new TestApiRepository();
            _communityRepository = new CommunityRepository();
            _testCommunityRepository = new TestCommunityRepository();
        }

        [TestMethod]
        public void GetFriendListSuccess()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = service.GetFriendListAsync(_steamId).Result;

            Assert.AreNotEqual(default(FriendsList), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Friends.Count > 0, "Expected at least one friend.");
        }

        [TestMethod]
        public void GetFriendListMockSuccess()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.GetFriendListAsync(_steamId).Result;

            Assert.AreNotEqual(default(FriendsList), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Friends.Count > 0, "Expected at least one friend.");
            Assert.AreEqual(34, model.Friends.Count, "Incorrect amount of friend items deserialized.");
            Assert.AreEqual("76561197961947067", model.Friends[0].SteamId, "SteamId incorrectly deserialized for the first item.");
        }

        [TestMethod]
        public void GetPlayerSummariesSuccess()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = service.GetPlayerSummariesAsync(new List<string> { _steamId }).Result;

            Assert.AreNotEqual(default(PlayerSummaries), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Players.Count > 0, "Expected at least one player.");
        }

        [TestMethod]
        public void GetPlayerSummariesMockSuccess()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.GetPlayerSummariesAsync(new List<string> { _steamId }).Result;

            Assert.AreNotEqual(default(PlayerSummaries), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Players.Count > 0, "Expected at least one player.");
            Assert.AreEqual(2, model.Players.Count, "Incorrect amount of player items deserialized.");
            Assert.AreEqual("76561198084024217", model.Players[0].SteamId, "SteamId incorrectly deserialized for the first item.");
        }

        [TestMethod]
        public void ResolveVanityUrlSuccess()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = service.ResolveVanityUrlAsync(_vanityUrl).Result;

            Assert.AreNotEqual(default(UserData), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Success == 1, "Expected a successful response.");
        }

        [TestMethod]
        public void ResolveVanityUrlMockSuccess()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.ResolveVanityUrlAsync(_vanityUrl).Result;

            Assert.AreNotEqual(default(UserData), model, "Deserialized object is empty.");
            Assert.IsTrue(model.Success == 1, "Expected a successful response.");
            Assert.AreEqual(1, model.Success, "Success code incorrectly deserialized.");
            Assert.AreEqual("76561197974081377", model.SteamId, "SteamId incorrectly deserialized.");
        }

        [TestMethod]
        public void FindUsersSuccess()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = service.FindUsersAsync("eadgar").Result;

            Assert.IsNotNull(model);
            Assert.IsTrue(model.Count > 0);
            Assert.IsNotNull(model[0].Name);
            Assert.IsNotNull(model[0].Username);
            Assert.IsNotNull(model[0].ProfileUrl);
            Assert.IsNotNull(model[0].AvatarUrl);
        }

        [TestMethod]
        public void FindUsersMockSuccess()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.FindUsersAsync("eadgar").Result;

            Assert.IsNotNull(model);
            Assert.IsTrue(model.Count > 0);
            Assert.AreEqual("Edgars Šults", model[0].Name);
            Assert.AreEqual("Eadgar", model[0].Username);
            Assert.AreEqual(_profileUrl, model[0].ProfileUrl);
            Assert.AreEqual("http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/6d/6dd0bc925d93a031b1493af1cbdb8b9d7550fce9_medium.jpg", model[0].AvatarUrl);
        }

        [TestMethod]
        public void GetSteamIdSuccess()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = service.GetSteamIdAsync(_profileUrl).Result;

            Assert.AreEqual(_steamId, model);
        }

        [TestMethod]
        public void GetSteamIdMockSuccess()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.GetSteamIdAsync(_profileUrl).Result;

            Assert.AreEqual(_steamId, model);
        }
    }
}
