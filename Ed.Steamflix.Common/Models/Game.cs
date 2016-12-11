using Newtonsoft.Json;
using Windows.ApplicationModel.Resources;

namespace Ed.Steamflix.Common.Models
{
    /// <summary>
    /// Represents a Steam game.
    /// </summary>
    public class Game
    {
        private readonly ResourceLoader _settings = ResourceLoader.GetForViewIndependentUse("Ed.Steamflix.Common/Settings");

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
        /// Store page logo URL, 460x215 px.
        /// </summary>
        public string StoreLogoUrl { get; set; }

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
                return string.IsNullOrEmpty(IconUrl) ? null : string.Format(_settings.GetString("ImageUrlFormat"), AppId, IconUrl);
            }
        }

        /// <summary>
        /// The program logo's file name, 184x69 px.
        /// </summary>
        /// <remarks>
        /// http://media.steampowered.com/steamcommunity/public/images/apps/{appid}/{hash}.jpg
        /// </remarks>
        [JsonProperty("img_logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Full logo URL.
        /// </summary>
        public string FormattedLogoUrl
        {
            get
            {
                var logoUrl = string.Empty;

                if (!string.IsNullOrEmpty(LogoUrl))
                {
                    // Logo from Steam API
                    logoUrl = string.Format(_settings.GetString("ImageUrlFormat"), AppId, LogoUrl);
                }
                else if (!string.IsNullOrEmpty(StoreLogoUrl))
                {
                    // Logo from app details call
                    logoUrl = StoreLogoUrl;
                }
                else
                {
                    // Store logo
                    logoUrl = $"http://cdn.akamai.steamstatic.com/steam/apps/{AppId}/header.jpg";
                }

                return logoUrl;
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
