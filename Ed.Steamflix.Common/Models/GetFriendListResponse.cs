using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class GetFriendListResponse
    {
        [JsonProperty("friendslist")]
        public FriendsList FriendsList { get; set; }
    }
}
