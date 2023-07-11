using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Ed.Steamflix.Tests.Services
{
    /// <summary>
    /// Tests SteamUser method implementations.
    /// </summary>
    public class UserServiceTests
    {
        private readonly string _steamId = "76561197974081377"; // Me
        private readonly string _vanityUrl = "edgarssults";
        private readonly string _profileUrl = "http://steamcommunity.com/id/edgarssults";
        private readonly IApiRepository _apiRepository;
        private readonly IApiRepository _testApiRepository;
        private readonly ICommunityRepository _communityRepository;
        private readonly ICommunityRepository _testCommunityRepository;

        public UserServiceTests()
        {
            _apiRepository = new ApiRepository();
            _testApiRepository = new TestApiRepository();
            _communityRepository = new CommunityRepository();
            _testCommunityRepository = new TestCommunityRepository();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetPlayerSummaries_Success()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = service.GetPlayerSummaries(new List<string> { _steamId }).Result;

            Assert.NotEqual(default, model);
            Assert.True(model.Players.Count > 0, "Expected at least one player.");
        }

        [Fact]
        public void GetPlayerSummaries_Mock_Success()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.GetPlayerSummaries(new List<string> { _steamId }).Result;

            Assert.NotEqual(default, model);
            Assert.True(model.Players.Count > 0, "Expected at least one player.");
            Assert.Equal(2, model.Players.Count);
            Assert.Equal("76561198084024217", model.Players[0].SteamId);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ResolveVanityUrl_Success()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = service.ResolveVanityUrl(_vanityUrl).Result;

            Assert.NotEqual(default, model);
            Assert.True(model.Success == 1, "Expected a successful response.");
        }

        [Fact]
        public void ResolveVanityUrl_Mock_Success()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.ResolveVanityUrl(_vanityUrl).Result;

            Assert.NotEqual(default, model);
            Assert.True(model.Success == 1, "Expected a successful response.");
            Assert.Equal(1, model.Success);
            Assert.Equal("76561197974081377", model.SteamId);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task FindUsers_Success()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = await service.FindUsers("eadgar");

            Assert.NotNull(model);
            Assert.True(model.Count > 0);
            Assert.NotNull(model[0].Name);
            Assert.NotNull(model[0].ProfileName);
            Assert.NotNull(model[0].ProfileUrl);
            Assert.NotNull(model[0].AvatarUrl);
            Assert.NotNull(model[0].Location);
            Assert.NotNull(model[0].LocationImageUrl);
        }

        [Fact]
        public void FindUsers_Mock_Success()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.FindUsers("eadgar").Result;

            Assert.NotNull(model);
            Assert.True(model.Count > 0);
            Assert.Equal("Edgars Šults", model[0].Name);
            Assert.Equal("Eadgar", model[0].ProfileName);
            Assert.Equal(_profileUrl, model[0].ProfileUrl);
            Assert.Equal("http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/6d/6dd0bc925d93a031b1493af1cbdb8b9d7550fce9_medium.jpg", model[0].AvatarUrl);
            Assert.Equal("Riga, Latvia", model[0].Location);
            Assert.NotNull(model[0].LocationImageUrl);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetSteamId_Success()
        {
            var service = new UserService(_apiRepository, _communityRepository);
            var model = service.GetSteamId(_profileUrl).Result;

            Assert.Equal(_steamId, model);
        }

        [Fact]
        public void GetSteamId_Mock_Success()
        {
            var service = new UserService(_testApiRepository, _testCommunityRepository);
            var model = service.GetSteamId(_profileUrl).Result;

            Assert.Equal(_steamId, model);
        }
    }
}
