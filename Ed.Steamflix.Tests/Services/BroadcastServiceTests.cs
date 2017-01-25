using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks.Repositories;
using System.Linq;
using Xunit;

namespace Ed.Steamflix.Tests.Services
{
    public class BroadcastServiceTests
    {
        private ICommunityRepository _communityRepository;
        private ICommunityRepository _testCommunityRepository;

        public BroadcastServiceTests()
        {
            _communityRepository = new CommunityRepository();
            _testCommunityRepository = new TestCommunityRepository();
        }

        [Fact]
        public void GetBroadcastsMockSuccess()
        {
            var service = new BroadcastService(_testCommunityRepository);
            var broadcasts = service.GetBroadcastsAsync(292030).Result; // Witcher 3

            Assert.Equal("The Witcher 3: Wild Hunt", broadcasts.GameName);
            Assert.Equal(10, broadcasts.Broadcasts.Count);
            Assert.Equal("sp1rt", broadcasts.Broadcasts[0].UserName);
            Assert.Equal("http://steamcommunity.com/broadcast/watch/76561197993685873", broadcasts.Broadcasts[0].WatchUrl);
            Assert.True(broadcasts.Broadcasts.Any(b => !string.IsNullOrEmpty(b.ImageUrl)), "No broadcasts have images.");
        }

        [Fact]
        public void GetBroadcastsSuccess()
        {
            var service = new BroadcastService(_communityRepository);
            var broadcasts = service.GetBroadcastsAsync(570).Result; // Dota 2

            Assert.Equal("Dota 2", broadcasts.GameName);
            Assert.True(broadcasts.Broadcasts.Count > 0, "Expected a different number of broadcasts.");
        }
    }
}
