using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    public class Player
    {
        /// <summary>
        /// 64bit SteamID of the user.
        /// </summary>
        [JsonProperty("steamid")]
        public string SteamId { get; set; }

        /// <summary>
        /// The player's persona name (display name).
        /// </summary>
        [JsonProperty("personaname")]
        public string PersonaName { get; set; }

        /// <summary>
        /// The full URL of the player's Steam Community profile.
        /// </summary>
        [JsonProperty("profileurl")]
        public string ProfileUrl { get; set; }

        /// <summary>
        /// The full URL of the player's 32x32px avatar.
        /// If the user hasn't configured an avatar, this will be the default ? avatar.
        /// </summary>
        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// The full URL of the player's 64x64px avatar.
        /// If the user hasn't configured an avatar, this will be the default ? avatar.
        /// </summary>
        [JsonProperty("avatarmedium")]
        public string AvatarMediumUrl { get; set; }

        /// <summary>
        /// The full URL of the player's 184x184px avatar.
        /// If the user hasn't configured an avatar, this will be the default ? avatar.
        /// </summary>
        [JsonProperty("avatarfull")]
        public string AvatarFullUrl { get; set; }

        /// <summary>
        /// The user's current status.
        /// </summary>
        /// <remarks>
        /// 0 - Offline, 1 - Online, 2 - Busy, 3 - Away, 4 - Snooze, 5 - looking to trade, 6 - looking to play.
        /// If the player's profile is private, this will always be "0".
        /// </remarks>
        [JsonProperty("personastate")]
        public int PersonaState { get; set; }

        /// <summary>
        /// This represents whether the profile is visible or not, and if it is visible, why you are allowed to see it.
        /// </summary>
        /// <remarks>
        /// Note that because this WebAPI does not use authentication, there are only two possible values returned:
        ///     1 - the profile is not visible to you (Private, Friends Only, etc),
        ///     3 - the profile is "Public", and the data is visible.
        /// </remarks>
        [JsonProperty("communityvisibilitystate")]
        public int CommunityVisibilityState { get; set; }

        /// <summary>
        /// If set, indicates the user has a community profile configured (will be set to '1').
        /// </summary>
        [JsonProperty("profilestate")]
        public int ProfileState { get; set; }

        /// <summary>
        /// The last time the user was online, in unix time.
        /// </summary>
        [JsonProperty("lastlogoff")]
        public int LastLogoff { get; set; }

        /// <summary>
        /// If set, indicates the profile allows public comments.
        /// </summary>
        /// <remarks>
        /// Set to what? Using string for now.
        /// </remarks>
        [JsonProperty("commentpermission")]
        public string CommentPermission { get; set; }

        /// <summary>
        /// The player's "Real Name", if they have set it and it is not private.
        /// </summary>
        [JsonProperty("realname")]
        public string RealName { get; set; }

        /// <summary>
        /// The player's primary group, as configured in their Steam Community profile. 
        /// </summary>
        [JsonProperty("primaryclanid")]
        public string PrimaryClanId { get; set; }

        /// <summary>
        /// The time the player's account was created.
        /// </summary>
        [JsonProperty("timecreated")]
        public int TimeCreated { get; set; }

        /// <summary>
        /// If the user is currently in-game, this value will be returned and set to the gameid of that game.
        /// </summary>
        [JsonProperty("gameid")]
        public int? GameId { get; set; }

        /// <summary>
        /// The ip and port of the game server the user is currently playing on,
        /// if they are playing on-line in a game using Steam matchmaking.
        /// Otherwise will be set to "0.0.0.0:0".
        /// </summary>
        [JsonProperty("gameserverip")]
        public string GameServerIp { get; set; }

        /// <summary>
        /// If the user is currently in-game, this will be the name of the game they are playing.
        /// </summary>
        /// <remarks>
        /// This may be the name of a non-Steam game shortcut.
        /// </remarks>
        [JsonProperty("gameextrainfo")]
        public string GameExtraInfo { get; set; }

        /// <summary>
        /// If set on the user's Steam Community profile, the user's country of residence, 2-character ISO country code.
        /// </summary>
        [JsonProperty("loccountrycode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// If set on the user's Steam Community profile, the user's state of residence.
        /// </summary>
        [JsonProperty("locstatecode")]
        public string StateCode { get; set; }

        /// <summary>
        /// An internal code indicating the user's city of residence.
        /// </summary>
        /// <remarks>
        /// https://github.com/Holek/steam-friends-countries
        /// </remarks>
        [JsonProperty("loccityid")]
        public int CityId { get; set; }
    }
}