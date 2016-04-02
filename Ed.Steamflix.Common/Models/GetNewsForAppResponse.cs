using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class GetNewsForAppResponse
    {
        [JsonProperty("appnews")]
        public AppNews AppNews { get; set; }
    }
}
