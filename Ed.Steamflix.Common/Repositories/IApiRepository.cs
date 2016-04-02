using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Repositories
{
    public interface IApiRepository
    {
        /// <summary>
        /// Reads JSON from a URL asynchronously.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>JSON string.</returns>
        Task<string> ReadUrlAsync(string url);

        /// <summary>
        /// Reads JSON from Steam API asynchronously.
        /// </summary>
        /// <param name="service">Steam service name, f.ex., IPlayerService.</param>
        /// <param name="method">Service method name, f.ex., GetRecentlyPlayedGames.</param>
        /// <param name="version">Method version, f.ex., v0001.</param>
        /// <param name="parameters">Additional parameters, f.ex., steamid=XYZ.</param>
        /// <returns>JSON string.</returns>
        Task<string> ApiCallAsync(string service, string method, string version, string parameters);
    }
}
