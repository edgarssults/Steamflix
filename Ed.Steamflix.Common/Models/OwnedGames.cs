using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.Models
{
    public class OwnedGames
    {
        [JsonProperty("game_count")]
        public int GameCount { get; set; }

        [JsonProperty("games")]
        public List<Game> Games { get; set; }
    }
}
