using Newtonsoft.Json;
using Ed.Steamflix.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ed.Steamflix.Common.Repositories;
using System.Text.RegularExpressions;
using System;
using System.Linq;

namespace Ed.Steamflix.Common.Services
{
    /// <summary>
    /// Service class for Steam's ISteamUser API.
    /// </summary>
    public class UserService
    {
        private readonly Regex _steamIdRegex = new Regex(@"steamcommunity\.com/profiles/(?<SteamId>[^/]*)", RegexOptions.Singleline);
        private readonly Regex _vanityUrlRegex = new Regex(@"steamcommunity\.com/id/(?<VanityUrlName>[^/]*)", RegexOptions.Singleline);
        private readonly string _servicename = "ISteamUser";

        private readonly IApiRepository _apiRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="apiRepository">API repository implementation.</param>
        public UserService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        /// <summary>
        /// Retrieves the list of friends for a user.
        /// </summary>
        /// <param name="steamId">User's Steam ID.</param>
        /// <returns>List of friends.</returns>
        public async Task<FriendsList> GetFriendListAsync(string steamId)
        {
            if (string.IsNullOrEmpty(steamId))
            {
                throw new ArgumentNullException(nameof(steamId));
            }

            var call = _apiRepository.ApiCallAsync(
                _servicename,
                "GetFriendList",
                "v0001",
                $"steamid={steamId}&relationship=friend"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<GetFriendListResponse>(await call);

            return model.FriendsList;
        }

        /// <summary>
        /// Retrieves a list of player summaries for the specified users.
        /// </summary>
        /// <param name="steamIds">
        /// Comma-delimited list of 64 bit Steam IDs to return profile information for.
        /// Up to 100 Steam IDs can be requested.
        /// </param>
        /// <returns>List of player summaries.</returns>
        public async Task<PlayerSummaries> GetPlayerSummariesAsync(List<string> steamIds)
        {
            if (steamIds == null || !steamIds.Any())
            {
                throw new ArgumentNullException(nameof(steamIds));
            }

            var call = _apiRepository.ApiCallAsync(
                _servicename,
                "GetPlayerSummaries",
                "v0002",
                $"steamids={string.Join(",", steamIds)}"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<GetPlayerSummariesResponse>(await call);

            return model.PlayerSummaries;
        }

        /// <summary>
        /// Resolves a vanity URL name to a Steam ID.
        /// </summary>
        /// <remarks>
        /// https://wiki.teamfortress.com/wiki/WebAPI/ResolveVanityUrl
        /// </remarks>
        /// <param name="vanityUrlName">
        /// The user's vanity URL that you would like to retrieve a Steam ID for,
        /// e.g. http://steamcommunity.com/id/edgarssults/ would use "edgarssults".
        /// </param>
        /// <returns>User info.</returns>
        public async Task<UserData> ResolveVanityUrlAsync(string vanityUrlName)
        {
            if (string.IsNullOrEmpty(vanityUrlName))
            {
                throw new ArgumentNullException(nameof(vanityUrlName));
            }

            var call = _apiRepository.ApiCallAsync(
                _servicename,
                "ResolveVanityUrl",
                "v0001",
                $"vanityurl={vanityUrlName}"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<ResolveVanityUrlResponse>(await call);

            return model.UserData;
        }

        /// <summary>
        /// Gets a Steam ID out of a Steam community profile URL.
        /// </summary>
        /// <param name="profileUrl">Steam community profile URL</param>
        /// <returns>Steam ID.</returns>
        public async Task<string> GetSteamIdAsync(string profileUrl)
        {
            if (string.IsNullOrEmpty(profileUrl))
            {
                throw new ArgumentNullException(nameof(profileUrl));
            }

            var steamIdMatch = _steamIdRegex.Match(profileUrl);
            var vanityUrlMatch = _vanityUrlRegex.Match(profileUrl);

            if (steamIdMatch.Success)
            {
                return steamIdMatch.Groups["SteamId"].Value;
            }
            else if (vanityUrlMatch.Success)
            {
                var userData = await ResolveVanityUrlAsync(vanityUrlMatch.Groups["VanityUrlName"].Value).ConfigureAwait(false);
                return userData.SteamId;
            }
            else
            {
                throw new Exception("Could not find Steam ID.");
            }
        }
    }
}