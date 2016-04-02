using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class ResolveVanityUrlResponse
    {
        [JsonProperty("response")]
        public UserData UserData { get; set; }
    }
}
