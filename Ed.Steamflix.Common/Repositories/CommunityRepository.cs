using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        private List<Cookie> _cookieCache = new List<Cookie>();

        /// <summary>
        /// Retrieves the broadcasts page HTML for a game asynchronously.
        /// </summary>
        /// <param name="appId">Application identifier.</param>
        /// <returns>HTML string.</returns>
        public async Task<string> GetBroadcastHtmlAsync(int appId)
        {
            var broadcastUrl = string.Format(Settings.BroadcastUrlFormat, appId);

            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(new Uri(broadcastUrl)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the Steam stats page HTML asynchronously.
        /// </summary>
        /// <returns>HTML string.</returns>
        public async Task<string> GetStatsHtmlAsync()
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(new Uri(Settings.StatsUrl)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the Steam community user search results page HTML asynchronously.
        /// </summary>
        /// <param name="user">Username to search for.</param>
        /// <param name="sessionId">Session ID value from cookie.</param>
        /// <param name="steamCountry">Steam country value from cookie.</param>
        /// <returns>HTML string.</returns>
        public async Task<string> GetUsersHtmlAsync(string user, string sessionId, string steamCountry)
        {
            var userSearchUrl = string.Format(Settings.UserSearchUrlFormat, user, sessionId);
            var steamCommunityUrl = Settings.SteamCommunityUrl;

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler())
            {
                handler.CookieContainer = cookieContainer;

                using (var client = new HttpClient(handler))
                {
                    cookieContainer.Add(new Uri(steamCommunityUrl), new Cookie(Settings.SessionIdCookie, sessionId));
                    cookieContainer.Add(new Uri(steamCommunityUrl), new Cookie(Settings.CountryCookie, steamCountry));
                    return await client.GetStringAsync(new Uri(userSearchUrl)).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Gets cookies set by steamcommunity.com.
        /// </summary>
        /// <returns>List of cookies.</returns>
        public async Task<List<Cookie>> GetSteamSetCookiesAsync()
        {
            if (_cookieCache.Any())
            {
                return _cookieCache;
            }

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(Settings.SteamCommunityUrl).ConfigureAwait(false))
                {
                    var cookieContainer = response.ReadCookies();
                    var cookies = cookieContainer.GetCookies(new Uri(Settings.SteamCommunityUrl)).Cast<Cookie>().ToList();

                    if (cookies.Any())
                    {
                        _cookieCache = cookies;
                    }

                    return cookies;
                }
            }
        }
    }
}