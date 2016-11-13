using System;
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
    }
}