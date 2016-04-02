using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class NewsItem
    {
        [JsonProperty("gid")]
        public string Gid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
