using Ed.Steamflix.Common.Repositories;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Ed.Steamflix.Tests
{
    // https://developer.valvesoftware.com/wiki/Steam_Web_API

    /// <summary>
    /// Represents a collection of tests for Steam API methods.
    /// Also tests the request service.
    /// </summary>
    [TestClass]
    public class ApiTests
    {
        private readonly string _steamId = "76561197974081377"; // Me
        private readonly string _otherSteamId = "76561198084024217"; // Someone else
        private readonly string _apiKey = Common.ApiHelper.SteamApiKey;
        private IApiRepository _apiRepository;

        [TestInitialize]
        public void Initialize()
        {
            _apiRepository = new ApiRepository();
        }

        [TestMethod]
        public void GetRecentlyPlayedGamesApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v1/?key={_apiKey}&steamid={_steamId}&format=json";
            var result = _apiRepository.ReadUrlAsync(requestUrl).Result;

            Assert.IsFalse(string.IsNullOrEmpty(result), "Result should not be empty.");
        }

        [TestMethod]
        public void GetOwnedGamesApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={_apiKey}&steamid={_steamId}&include_appinfo=1&format=json";
            var result = _apiRepository.ReadUrlAsync(requestUrl).Result;

            Assert.IsFalse(string.IsNullOrEmpty(result), "Result should not be empty.");
        }

        [TestMethod]
        public void GetFriendListApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key={_apiKey}&steamid={_steamId}&relationship=friend&format=json";
            var result = _apiRepository.ReadUrlAsync(requestUrl).Result;

            Assert.IsFalse(string.IsNullOrEmpty(result), "Result should not be empty.");
        }

        [TestMethod]
        public void GetPlayerSummariesApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_apiKey}&steamids={_steamId + "," + _otherSteamId}&format=json";
            var result = _apiRepository.ReadUrlAsync(requestUrl).Result;

            Assert.IsFalse(string.IsNullOrEmpty(result), "Result should not be empty.");
        }

        [TestMethod]
        public void ResolveVanityUrlApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key={_apiKey}&vanityurl=edgarssults&format=json";
            var result = _apiRepository.ReadUrlAsync(requestUrl).Result;

            Assert.IsFalse(string.IsNullOrEmpty(result), "Result should not be empty.");
        }
    }
}