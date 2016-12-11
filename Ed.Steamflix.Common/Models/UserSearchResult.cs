using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class UserSearchResult
    {
        /// <summary>
        /// Gets or sets a value indicating success (1) or failure
        /// </summary>
        [JsonProperty("success")]
        public int Success { get; set; }        

        /// <summary>
        /// Gets or sets user search response html
        /// </summary>
        [JsonProperty("html")]
        public string Html { get; set; }
    }
}
