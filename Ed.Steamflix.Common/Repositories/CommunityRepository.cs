using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        /// <summary>
        /// Retrieves the broadcasts page HTML for a game asynchronously.
        /// </summary>
        /// <param name="appId">Application identifier.</param>
        /// <returns></returns>
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
        /// <returns></returns>
        public async Task<string> GetStatsHtmlAsync()
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(new Uri(Settings.StatsUrl)).ConfigureAwait(false);
            }
        }
    }
}