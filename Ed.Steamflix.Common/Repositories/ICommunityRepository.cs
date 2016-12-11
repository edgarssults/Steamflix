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
        /// <returns></returns>
        Task<string> GetStatsHtmlAsync();

        /// <summary>
        /// Retrieves the steam community user search results page asynchonously.
        /// </summary>
        /// <param name="user">Username to search for</param>
        /// <param name="sessionId">Session id cookie</param>
        /// <param name="steamCountry">Steam country cookie</param>
        /// <returns>The response body</returns>
        Task<string> FindUsersAsync(string user, string sessionId, string steamCountry);

        /// <summary>
        /// Gets cookies set by steamcommunity.com
        /// </summary>
        /// <returns>String of set cookies</returns>
        Task<string> GetSteamSetCookiesAsync();
    }
}
