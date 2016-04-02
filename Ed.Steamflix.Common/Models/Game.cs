using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    /// <summary>
    /// Represents a Steam game.
    /// </summary>
    public class Game
    {
        // TODO: Settings/resources
        private readonly string _apiIconUrlFormat = "http://media.steampowered.com/steamcommunity/public/images/apps/{0}/{1}.jpg";

        /// <summary>
        /// An integer containing the program's ID.
        /// </summary>
        [JsonProperty("appid")]
        public int AppId { get; set; }

        /// <summary>
        /// A string containing the program's publicly facing title.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The program icon's file name.
        /// </summary>
        /// <remarks>
        /// http://media.steampowered.com/steamcommunity/public/images/apps/{appid}/{hash}.jpg
        /// </remarks>
        [JsonProperty("img_icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// Full icon URL, 32x32 px.
        /// </summary>
        public string FormattedIconUrl
        {
            get
            {
                return string.Format(_apiIconUrlFormat, AppId, IconUrl);
            }
        }

        /// <summary>
        /// The program logo's file name.
        /// </summary>
        /// <remarks>
        /// http://media.steampowered.com/steamcommunity/public/images/apps/{appid}/{hash}.jpg
        /// </remarks>
        [JsonProperty("img_logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Full logo URL, 184x69 px.
        /// </summary>
        public string FormattedLogoUrl
        {
            get
            {
                return string.Format(_apiIconUrlFormat, AppId, LogoUrl);
            }
        }

        /// <summary>
        /// An integer of the the player's total playtime, denoted in minutes.
        /// </summary>
        [JsonProperty("playtime_forever")]
        public int PlaytimeForever { get; set; }

        /// <summary>
        /// An integer of the player's playtime in the past 2 weeks, denoted in minutes.
        /// </summary>
        [JsonProperty("playtime_2weeks")]
        public int PlaytimeTwoWeeks { get; set; }

        /// <summary>
        /// Whether the program has stats accessible via GetUserStatsForGame and GetGlobalStatsForGame.
        /// </summary>
        [JsonProperty("has_community_visible_stats")]
        public bool HasCommunityVisibleStats { get; set; }
    }
}
