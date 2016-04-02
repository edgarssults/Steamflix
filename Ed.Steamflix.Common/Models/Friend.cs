using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class Friend
    {
        /// <summary>
        /// 64 bit Steam ID of the friend.
        /// </summary>
        [JsonProperty("steamid")]
        public string SteamId { get; set; }

        /// <summary>
        /// Relationship qualifier.
        /// </summary>
        [JsonProperty("relationship")]
        public string Relationship { get; set; }

        /// <summary>
        /// Unix timestamp of the time when the relationship was created.
        /// </summary>
        [JsonProperty("friend_since")]
        public int FriendSince { get; set; }
    }
}