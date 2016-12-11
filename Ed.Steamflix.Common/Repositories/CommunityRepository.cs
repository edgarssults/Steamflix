using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Ed.Steamflix.Common.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly ResourceLoader _settings = new ResourceLoader("Ed.Steamflix.Common/Settings");

        /// <summary>
        /// Retrieves the broadcasts page HTML for a game asynchronously.
        /// </summary>
        /// <param name="appId">Application identifier.</param>
        /// <returns></returns>
        public async Task<string> GetBroadcastHtmlAsync(int appId)
        {
            var broadcastUrl = string.Format(_settings.GetString("BroadcastUrlFormat"), appId);

            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(new Uri(broadcastUrl)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the Steam stats page HTML asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetStatsHtmlAsync()
        {
            var statsUrl = _settings.GetString("StatsUrl");

            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(new Uri(statsUrl)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the steam community user search results page asynchonously.
        /// </summary>
        /// <param name="user">Username to search for</param>
        /// <param name="sessionId">Session id cookie</param>
        /// <param name="steamCountry">Steam country cookie</param>
        /// <returns>The response body</returns>
        public async Task<string> FindUsersAsync(string user, string sessionId, string steamCountry)
        {
            var userSearchUrl = string.Format(_settings.GetString("UserSearchUrlFormat"), user, sessionId);
            var steamCommunityUrl = _settings.GetString("SteamCommunityUrl");

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {
                cookieContainer.Add(new Uri(steamCommunityUrl), new Cookie(_settings.GetString("SessionCookie"), sessionId));
                cookieContainer.Add(new Uri(steamCommunityUrl), new Cookie(_settings.GetString("CountryCookie"), steamCountry));
                return await client.GetStringAsync(new Uri(userSearchUrl)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets cookies set by steamcommunity.com
        /// </summary>
        /// <returns>String of set cookies</returns>
        public async Task<string> GetSteamSetCookiesAsync()
        {
            var request = HttpWebRequest.Create(_settings.GetString("SteamCommunityUrl")) as HttpWebRequest;
            var response = await request.GetResponseAsync() as HttpWebResponse;
            return response.Headers[HttpResponseHeader.SetCookie];
        }
    }
}