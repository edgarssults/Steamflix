using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Services
{
    /// <summary>
    /// Steam's ISteamUser API interaction logic.
    /// </summary>
    public class UserService
    {
        private readonly Regex _steamIdRegex = new Regex(@"steamcommunity\.com/profiles/(?<SteamId>[^/]*)", RegexOptions.Singleline);
        private readonly Regex _vanityUrlRegex = new Regex(@"steamcommunity\.com/id/(?<VanityUrlName>[^/]*)", RegexOptions.Singleline);
        private readonly Regex _userSearchRegex = new Regex(@"<div[^>]*class=""search_row"".*?<div[^>]*class=""avatarMedium""[^>]*>\s*<a[^>]*href=""(?<ProfileUrl>[^""]*)""[^>]*>\s*<img[^>]*src=""(?<AvatarUrl>[^""]*)""[^>]*>.*?<div[^>]*class=""searchPersonaInfo""[^>]*>\s*<a[^>]*>(?<ProfileName>[^<]+)</a>\s*(<br[^>]*>\s*(?<Name>[^<]+)<br[^>]*>\s*(?<Location>[^<]+)<img[^>]*src=""(?<LocationImageUrl>[^""]*)""[^>]*>\s*</div>|<br[^>]*>\s*(?<Name>[^<]+)<br[^>]*>\s*</div>|<br[^>]*>\s*(?<Location>[^<]+)<img[^>]*>\s*</div>)?", RegexOptions.Singleline);
        private readonly string _servicename = "ISteamUser";

        private readonly IApiRepository _apiRepository;
        private readonly ICommunityRepository _communityRepository;

        /// <summary>
        /// Steam's ISteamUser API interaction logic.
        /// </summary>
        /// <param name="apiRepository">API repository implementation.</param>
        /// <param name="communityRepository">Community repository implementation.</param>
        public UserService(IApiRepository apiRepository, ICommunityRepository communityRepository)
        {
            _apiRepository = apiRepository;
            _communityRepository = communityRepository;
        }

        /// <summary>
        /// Retrieves a list of player summaries for the specified users.
        /// </summary>
        /// <param name="steamIds">
        /// Comma-delimited list of 64 bit Steam IDs to return profile information for.
        /// Up to 100 Steam IDs can be requested.
        /// </param>
        /// <returns>List of player summaries.</returns>
        public async Task<PlayerSummaries> GetPlayerSummaries(List<string> steamIds)
        {
            if (steamIds == null || !steamIds.Any())
            {
                throw new ArgumentNullException(nameof(steamIds));
            }

            var response = await _apiRepository.ApiCall(
                _servicename,
                "GetPlayerSummaries",
                "v0002",
                $"steamids={string.Join(",", steamIds)}"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<GetPlayerSummariesResponse>(response);

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
        public async Task<UserData> ResolveVanityUrl(string vanityUrlName)
        {
            if (string.IsNullOrEmpty(vanityUrlName))
            {
                throw new ArgumentNullException(nameof(vanityUrlName));
            }

            var response = await _apiRepository.ApiCall(
                _servicename,
                "ResolveVanityUrl",
                "v0001",
                $"vanityurl={vanityUrlName}"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<ResolveVanityUrlResponse>(response);

            return model.UserData;
        }

        /// <summary>
        /// Gets a Steam ID out of a Steam community profile URL.
        /// </summary>
        /// <param name="profileUrl">Steam community profile URL</param>
        /// <returns>Steam ID.</returns>
        public async Task<string> GetSteamId(string profileUrl)
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
                var userData = await ResolveVanityUrl(vanityUrlMatch.Groups["VanityUrlName"].Value).ConfigureAwait(false);
                return userData.SteamId;
            }
            else
            {
                throw new Exception("Could not find Steam ID.");
            }
        }

        /// <summary>
        /// Executes a Steam community user search and processes the results.
        /// </summary>
        /// <param name="username">Username to search for.</param>
        /// <returns>List of users.</returns>
        public async Task<List<User>> FindUsers(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new List<User>();
            }

            var users = new List<User>();

            var steamCookies = await _communityRepository.GetSteamCookies().ConfigureAwait(false);
            if (steamCookies != null && steamCookies.Any())
            {
                var sessionIdCookie = steamCookies.FirstOrDefault(c => c.Name.Equals(Settings.SessionIdCookie));
                var steamCountryCookie = steamCookies.FirstOrDefault(c => c.Name.Equals(Settings.CountryCookie));

                if (sessionIdCookie != null && steamCountryCookie != null)
                {
                    var searchResults = await _communityRepository.GetUsersHtml(username, sessionIdCookie.Value, steamCountryCookie.Value).ConfigureAwait(false);
                    var result = JsonConvert.DeserializeObject<UserSearchResult>(searchResults);
                    if (result?.Success == 1)
                    {
                        users = ExtractUsers(result.Html.Replace(Environment.NewLine, string.Empty).Replace("\t", string.Empty));
                    }
                }
            }

            return users;
        }

        /// <summary>
        /// Extracts user information from user search results HTML with RegEx.
        /// </summary>
        /// <param name="html">User search results HTML.</param>
        /// <returns>List of users.</returns>
        private List<User> ExtractUsers(string html)
        {
            if (html.Contains("search_results_error"))
            {
                return new List<User>();
            }

            html = html.Replace("&nbsp;", "");
            var foundUsers = new List<User>();

            foreach (Match match in _userSearchRegex.Matches(html))
            {
                foundUsers.Add(new User()
                {
                    ProfileName = match.Groups["ProfileName"]?.Value.Trim(),
                    ProfileUrl = match.Groups["ProfileUrl"]?.Value.Trim(),
                    Name = match.Groups["Name"]?.Value.Trim(),
                    AvatarUrl = match.Groups["AvatarUrl"]?.Value.Trim(),
                    Location = match.Groups["Location"]?.Value.Trim(),
                    LocationImageUrl = match.Groups["LocationImageUrl"]?.Value.Trim()
                });
            }

            return foundUsers;
        }
    }
}