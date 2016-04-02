using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class GetPlayerSummariesResponse
    {
        [JsonProperty("response")]
        public PlayerSummaries PlayerSummaries { get; set; }
    }
}