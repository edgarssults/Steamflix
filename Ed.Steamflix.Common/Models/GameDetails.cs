using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class GameDetails
    {
        /// <summary>
        /// A string containing the program's publicly facing title.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// An integer containing the program's ID.
        /// </summary>
        [JsonProperty("steam_appid")]
        public int AppId { get; set; }

        /// <summary>
        /// The app's store page header image.
        /// </summary>
        /// <remarks>
        /// http://cdn.akamai.steamstatic.com/steam/apps/72850/header.jpg?t=1477612894
        /// </remarks>
        [JsonProperty("header_image")]
        public string LogoUrl { get; set; }
    }
}
