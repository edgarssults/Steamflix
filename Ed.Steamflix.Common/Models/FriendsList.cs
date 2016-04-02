using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.Models
{
    public class FriendsList
    {
        [JsonProperty("friends")]
        public List<Friend> Friends { get; set; }
    }
}