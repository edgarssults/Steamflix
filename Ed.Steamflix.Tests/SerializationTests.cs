using Ed.Steamflix.Common.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;

namespace Ed.Steamflix.Tests
{
    /// <summary>
    /// Represents a collection of tests of JSON serialization to and from Steam models.
    /// </summary>
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void GetNewsForAppResponseDeserializeSuccess()
        {
            var json = Mocks.Resources.GetNewsForAppResponseJson;
            var model = JsonConvert.DeserializeObject<GetNewsForAppResponse>(json);

            Assert.AreNotEqual(default(GetNewsForAppResponse), model, "Deserialized response is empty.");
            Assert.AreNotEqual(default(AppNews), model.AppNews, "Deserialized items object is empty.");
            Assert.AreEqual(440, model.AppNews.AppId, "AppId incorrectly deserialized.");
            Assert.AreEqual(5, model.AppNews.NewsItems.Count, "Incorrect amount of news items deserialized.");
            Assert.AreEqual("552009088092503181", model.AppNews.NewsItems[0].Gid, "GID incorrectly deserialized for the first item.");
        }
    }
}
