using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace Ed.Steamflix.Common.Repositories
{
    // https://wiki.teamfortress.com/wiki/WebAPI

    public class ApiRepository : IApiRepository
    {
        // TODO: Settings/resources
        private readonly string _apiUrl = "http://api.steampowered.com";
        private readonly string _apiKey = "8FAC8288FB26E59C1468DAD0DFED2683";

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
            return await ReadUrlAsync($"{_apiUrl}/{service}/{method}/{version}/?key={_apiKey}&format=json&{parameters}").ConfigureAwait(false);
        }
    }
}