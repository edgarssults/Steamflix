using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        // TODO: Settings/Resources
        private readonly string _broadcastUrlFormat = "http://steamcommunity.com/app/{0}/broadcasts/";

        /// <summary>
        /// Retrieves the broadcasts page HTML for a game asynchronously.
        /// </summary>
        /// <param name="appId">Application identifier.</param>
        /// <returns></returns>
        public async Task<string> GetBroadcastHtmlAsync(int appId)
        {
            var broadcastUrl = string.Format(_broadcastUrlFormat, appId);

            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(new Uri(broadcastUrl)).ConfigureAwait(false);
            }
        }
    }
}