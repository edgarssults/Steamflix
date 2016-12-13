using Ed.Steamflix.Common.Models;
using Newtonsoft.Json;
using Xunit;

namespace Ed.Steamflix.Tests
{
    /// <summary>
    /// Represents a collection of tests of JSON serialization to and from Steam models.
    /// </summary>
    public class SerializationTests
    {
        [Fact]
        public void GetNewsForAppResponseDeserializeSuccess()
        {
            var json = Mocks.Resources.GetNewsForAppResponseJson;
            var model = JsonConvert.DeserializeObject<GetNewsForAppResponse>(json);

            Assert.NotEqual(default(GetNewsForAppResponse), model);
            Assert.NotEqual(default(AppNews), model.AppNews);
            Assert.Equal(440, model.AppNews.AppId);
            Assert.Equal(5, model.AppNews.NewsItems.Count);
            Assert.Equal("552009088092503181", model.AppNews.NewsItems[0].Gid);
        }
    }
}
