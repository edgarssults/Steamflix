using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Repositories
{
    /// <summary>
    /// Steam Community interaction logic.
    /// </summary>
    public interface ICommunityRepository
    {
        /// <summary>
        /// Retrieves the broadcasts page HTML for a game.
        /// </summary>
        /// <param name="appId">Application identifier.</param>
        /// <returns>HTML string.</returns>
        Task<string> GetBroadcastHtml(int appId);

        /// <summary>
        /// Retrieves the Steam stats page HTML.
        /// </summary>
        /// <returns>HTML string.</returns>
        Task<string> GetStatsHtml();

        /// <summary>
        /// Retrieves the Steam community user search results page HTML.
        /// </summary>
        /// <param name="user">Username to search for.</param>
        /// <param name="sessionId">Session ID value from cookie.</param>
        /// <param name="steamCountry">Steam country value from cookie.</param>
        /// <returns>HTML string.</returns>
        Task<string> GetUsersHtml(string user, string sessionId, string steamCountry);

        /// <summary>
        /// Gets cookies set by steamcommunity.com.
        /// </summary>
        /// <returns>List of cookies.</returns>
        Task<List<Cookie>> GetSteamCookies();
    }
}
