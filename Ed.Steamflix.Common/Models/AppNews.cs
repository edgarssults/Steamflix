using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.Models
{
    public class AppNews
    {
        [JsonProperty("appid")]
        public int AppId { get; set; }

        [JsonProperty("newsitems")]
        public List<NewsItem> NewsItems { get; set; }
    }
}
