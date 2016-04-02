using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class GetRecentlyPlayedGamesResponse
    {
        [JsonProperty("response")]
        public RecentlyPlayedGames RecentlyPlayedGames { get; set; }
    }
}
