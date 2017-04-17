using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks;
using Ed.Steamflix.Mocks.Repositories;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ed.Steamflix.Tests.Services
{
    public class BroadcastServiceTests
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly Mock<ICommunityRepository> _communityRepositoryMock;
        private readonly BroadcastService _target;
        private readonly BroadcastService _targetReal;

        public BroadcastServiceTests()
        {
            _communityRepository = new CommunityRepository();
            _communityRepositoryMock = new Mock<ICommunityRepository>();

            _communityRepositoryMock.Setup(m => m.GetBroadcastHtmlAsync(It.IsAny<int>()))
                .Returns((int appId) => { return Task.FromResult(Resources.ResourceManager.GetString("BroadcastsHtml" + appId)); });

            _targetReal = new BroadcastService(_communityRepository);
            _target = new BroadcastService(_communityRepositoryMock.Object);
        }

        [Fact]
        public void GetBroadcasts_Success()
        {
            var broadcasts = _target.GetBroadcastsAsync(292030).Result; // Witcher 3

            Assert.Equal("The Witcher 3: Wild Hunt", broadcasts.GameName);
            Assert.Equal(10, broadcasts.Broadcasts.Count);
            Assert.Equal("sp1rt", broadcasts.Broadcasts[0].UserName);
            Assert.Equal("http://steamcommunity.com/broadcast/watch/76561197993685873", broadcasts.Broadcasts[0].WatchUrl);
            Assert.True(broadcasts.Broadcasts.Any(b => !string.IsNullOrEmpty(b.ImageUrl)), "No broadcasts have images.");
            Assert.Equal(1, broadcasts.Broadcasts[0].ViewerCount);
        }

        [Fact]
        public void GetBroadcasts_Real_Success()
        {
            var broadcasts = _targetReal.GetBroadcastsAsync(570).Result; // Dota 2

            Assert.Equal("Dota 2", broadcasts.GameName);
            Assert.True(broadcasts.Broadcasts.Count > 0, "Expected a different number of broadcasts.");
            Assert.NotNull(broadcasts.Broadcasts[0].UserName);
            Assert.NotNull(broadcasts.Broadcasts[0].WatchUrl);
            Assert.NotNull(broadcasts.Broadcasts[0].ViewerCount);
        }
    }
}
