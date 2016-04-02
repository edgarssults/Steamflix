using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.Models
{
    public class PlayerSummaries
    {
        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }
}