using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class UserData
    {
        /// <summary>
        /// The status of the request.
        /// 1 if successful, 42 if there was no match.
        /// </summary>
        [JsonProperty("success")]
        public int Success { get; set; }

        /// <summary>
        /// The 64 bit Steam ID the vanity URL resolves to.
        /// Not returned on resolution failures.
        /// </summary>
        [JsonProperty("steamid")]
        public string SteamId { get; set; }

        /// <summary>
        /// The message associated with the request status.
        /// Currently only used on resolution failures.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
