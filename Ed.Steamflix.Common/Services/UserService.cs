using Newtonsoft.Json;
using Ed.Steamflix.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ed.Steamflix.Common.Repositories;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Collections;

namespace Ed.Steamflix.Common.Services
{
    /// <summary>
    /// Service class for Steam's ISteamUser API.
    /// </summary>
    public class UserService
    {
        private readonly Regex _steamIdRegex = new Regex(@"steamcommunity\.com/profiles/(?<SteamId>[^/]*)", RegexOptions.Singleline);
        private readonly Regex _vanityUrlRegex = new Regex(@"steamcommunity\.com/id/(?<VanityUrlName>[^/]*)", RegexOptions.Singleline);
        private readonly Regex _userSearchRegex = new Regex(@"<div[^>]*class=""avatarMedium"".*?<img.*?src=""(?<image>[^""]*)"".*?>.*?<div[^>]*class=""searchPersonaInfo"".*?<a[^>]*class=""searchPersonaName""[^>]*href=""(?<profile>[^""]*)[^>]*>(?<username>[^<]*).*?</a>.*?<br />(?<name>[^<]*)", RegexOptions.Singleline);
        private readonly string _servicename = "ISteamUser";
        private readonly string _sessionId = "sessionid";
        private readonly string _steamCountry = "steamCountry";

        private readonly IApiRepository _apiRepository;
        private readonly ICommunityRepository _communityRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="apiRepository">API repository implementation.</param>
        /// <param name="communityRepository">Community repository implementation.</param>
        public UserService(IApiRepository apiRepository, ICommunityRepository communityRepository)
        {
            _apiRepository = apiRepository;
            _communityRepository = communityRepository;
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

        /// <summary>
        /// Executes the steamcommunity user search and processes the results
        /// </summary>
        /// <param name="user">User to search for</param>
        /// <returns>List of users</returns>
        public async Task<List<User>> FindUsersAsync(string user)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException(nameof(user));
            }

            var users = new List<User>();
            var steamSetCookies = await _communityRepository.GetSteamSetCookiesAsync().ConfigureAwait(false);
            if (!string.IsNullOrEmpty(steamSetCookies))
            {
                // Extracts the sessionId and steamCountry cookies from the string result
                var setCookies = steamSetCookies.Split(';');
                var sessionId = this.getCookie(setCookies, _sessionId);
                var steamCountry = this.getCookie(setCookies, _steamCountry);

                if (!string.IsNullOrEmpty(sessionId) && !string.IsNullOrEmpty(steamCountry))
                {
                    var searchResults = await _communityRepository.FindUsersAsync(user, sessionId, steamCountry).ConfigureAwait(false);
                    var result = JsonConvert.DeserializeObject<UserSearchResult>(searchResults);
                    if (result?.Success == 1)
                    {
                        users = this.extractUsers(result.Html.Replace(Environment.NewLine, string.Empty)
                                                             .Replace("\t", string.Empty));
                    }
                }
            }

            return users;
        }

        /// <summary>
        /// Gets needed cookie value out of an array of cookies
        /// </summary>
        /// <param name="setCookies">Array of cookies</param>
        /// <param name="cookieName">Cookie name to get value for</param>
        /// <returns>Cookie value</returns>
        private string getCookie(string[] setCookies, string cookieName)
        {
            if (setCookies != null && !string.IsNullOrEmpty(cookieName))
            {
                foreach (var item in setCookies)
                {
                    var temp = item.Split('=');
                    if (temp.Length == 2 && temp[0] == cookieName)
                    {
                        return temp[1];
                    }
                    else if (temp.Length == 3 && temp[1].Contains(cookieName))
                    {
                        return temp[2];
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Extracts user information from user search results html with regex
        /// </summary>
        /// <param name="html">User search results</param>
        /// <returns>List of found users</returns>
        private List<User> extractUsers(string html)
        {
            if (html.Contains("search_results_error"))
            {
                return new List<User>();
            }

            var foundUsers = new List<User>();
            foreach (Match match in _userSearchRegex.Matches(html))
            {
                foundUsers.Add(new User()
                {
                    Username = match.Groups["username"]?.Value,
                    ProfileUrl = match.Groups["profile"]?.Value,
                    Name = match.Groups["name"]?.Value,
                    AvatarUrl = match.Groups["image"]?.Value
                });
            }

            return foundUsers;
        }
    }
}