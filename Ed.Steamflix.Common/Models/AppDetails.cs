using Ed.Steamflix.Common.Models;
using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class AppDetails
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public GameDetails Game { get; set; }
    }
}
