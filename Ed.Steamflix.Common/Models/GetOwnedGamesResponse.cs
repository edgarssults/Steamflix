using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class GetOwnedGamesResponse
    {
        [JsonProperty("response")]
        public OwnedGames OwnedGames { get; set; }
    }
}
