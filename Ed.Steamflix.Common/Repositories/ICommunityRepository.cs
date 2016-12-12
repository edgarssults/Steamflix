using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Repositories
{
    public interface ICommunityRepository
    {
        /// <summary>
        /// Retrieves the broadcasts page HTML for a game asynchronously.
        /// </summary>
        /// <param name="appId">Application identifier.</param>
        /// <returns>HTML string.</returns>
        Task<string> GetBroadcastHtmlAsync(int appId);

        /// <summary>
        /// Retrieves the Steam stats page HTML asynchronously.
        /// </summary>
        /// <returns>HTML string.</returns>
        Task<string> GetStatsHtmlAsync();

        /// <summary>
        /// Retrieves the Steam community user search results page HTML asynchronously.
        /// </summary>
        /// <param name="user">Username to search for.</param>
        /// <param name="sessionId">Session ID value from cookie.</param>
        /// <param name="steamCountry">Steam country value from cookie.</param>
        /// <returns>HTML string.</returns>
        Task<string> GetUsersHtmlAsync(string user, string sessionId, string steamCountry);

        /// <summary>
        /// Gets cookies set by steamcommunity.com.
        /// </summary>
        /// <returns>List of cookies.</returns>
        Task<List<Cookie>> GetSteamSetCookiesAsync();
    }
}
