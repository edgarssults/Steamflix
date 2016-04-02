using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Mocks.Repositories;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Linq;

namespace Ed.Steamflix.Tests.Services
{
    [TestClass]
    public class BroadcastServiceTests
    {
        [TestMethod]
        public void GetBroadcastsMockSuccess()
        {
            var communityRepository = new TestCommunityRepository();
            var service = new BroadcastService(communityRepository);
            var broadcasts = service.GetBroadcastsAsync(292030).Result; // Witcher 3

            Assert.AreEqual(10, broadcasts.Count, "Expected a different number of broadcasts.");
            Assert.AreEqual("sp1rt", broadcasts[0].UserName, "First broadcast's user name not parsed correctly.");
            Assert.AreEqual("http://steamcommunity.com/broadcast/watch/76561197993685873", broadcasts[0].WatchUrl, "First broadcast's watch URL not parsed correctly.");
            Assert.IsTrue(broadcasts.Any(b => !string.IsNullOrEmpty(b.ImageUrl)), "No broadcasts have images.");
        }

        [TestMethod]
        public void GetBroadcastsSuccess()
        {
            var communityRepository = new CommunityRepository();
            var service = new BroadcastService(communityRepository);
            var broadcasts = service.GetBroadcastsAsync(570).Result; // Dota 2

            Assert.IsTrue(broadcasts.Count > 0, "Expected a different number of broadcasts.");
        }
    }
}
