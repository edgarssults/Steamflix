using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace Ed.Steamflix.Common.Repositories
{
    // https://wiki.teamfortress.com/wiki/WebAPI

    public class ApiRepository : IApiRepository
    {
        /// <summary>
        /// Reads JSON from a URL asynchronously.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>JSON string.</returns>
        public async Task<string> ReadUrlAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return await client.GetStringAsync(new Uri(url)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Reads JSON from Steam API asynchronously.
        /// </summary>
        /// <param name="service">Steam service name, f.ex., IPlayerService.</param>
        /// <param name="method">Service method name, f.ex., GetRecentlyPlayedGames.</param>
        /// <param name="version">Method version, f.ex., v0001.</param>
        /// <param name="parameters">Additional parameters, f.ex., steamid=XYZ.</param>
        /// <returns>JSON string.</returns>
        public async Task<string> ApiCallAsync(string service, string method, string version, string parameters)
        {
            return await ReadUrlAsync($"{Settings.ApiUrl}/{service}/{method}/{version}/?key={Settings.ApiKey}&format=json&{parameters}").ConfigureAwait(false);
        }
    }
}