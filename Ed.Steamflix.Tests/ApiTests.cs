using Ed.Steamflix.Common.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Ed.Steamflix.Tests
{
    // https://developer.valvesoftware.com/wiki/Steam_Web_API

    /// <summary>
    /// Represents a collection of tests for Steam API methods.
    /// Also tests the request service.
    /// </summary>
    public class ApiTests
    {
        private readonly string _steamId = "76561197974081377"; // Me
        private readonly string _otherSteamId = "76561198084024217"; // Someone else
        private readonly string _apiKey = Common.ApiHelper.SteamApiKey;
        private IApiRepository _apiRepository;

        public ApiTests()
        {
            _apiRepository = new ApiRepository();
        }

        [Fact]
        public async Task GetRecentlyPlayedGamesApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v1/?key={_apiKey}&steamid={_steamId}&format=json";
            var result = await _apiRepository.ReadUrl(requestUrl);

            Assert.False(string.IsNullOrEmpty(result), "Result should not be empty.");
        }

        [Fact]
        public async Task GetOwnedGamesApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={_apiKey}&steamid={_steamId}&include_appinfo=1&format=json";
            var result = await _apiRepository.ReadUrl(requestUrl);

            Assert.False(string.IsNullOrEmpty(result), "Result should not be empty.");
        }

        [Fact]
        public async Task GetFriendListApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key={_apiKey}&steamid={_steamId}&relationship=friend&format=json";
            var result = await _apiRepository.ReadUrl(requestUrl);

            Assert.False(string.IsNullOrEmpty(result), "Result should not be empty.");
        }

        [Fact]
        public async Task GetPlayerSummariesApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_apiKey}&steamids={_steamId + "," + _otherSteamId}&format=json";
            var result = await _apiRepository.ReadUrl(requestUrl);

            Assert.False(string.IsNullOrEmpty(result), "Result should not be empty.");
        }

        [Fact]
        public async Task ResolveVanityUrlApiRequestSuccess()
        {
            var requestUrl = $"http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key={_apiKey}&vanityurl=edgarssults&format=json";
            var result = await _apiRepository.ReadUrl(requestUrl);

            Assert.False(string.IsNullOrEmpty(result), "Result should not be empty.");
        }
    }
}