using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.Models
{
    public class RecentlyPlayedGames
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("games")]
        public List<Game> Games { get; set; }
    }
}
