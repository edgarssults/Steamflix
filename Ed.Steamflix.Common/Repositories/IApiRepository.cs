using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Repositories
{
    /// <summary>
    /// Steam API interaction logic.
    /// </summary>
    public interface IApiRepository
    {
        /// <summary>
        /// Reads JSON from a URL.
        /// </summary>
        /// <param name="url">URL to read from.</param>
        /// <returns>JSON string.</returns>
        Task<string> ReadUrl(string url);

        /// <summary>
        /// Reads JSON from Steam API.
        /// </summary>
        /// <param name="service">Steam service name, f.ex. IPlayerService.</param>
        /// <param name="method">Service method name, f.ex. GetRecentlyPlayedGames.</param>
        /// <param name="version">Method version, f.ex. v0001.</param>
        /// <param name="parameters">Additional parameters, f.ex. steamid=XYZ.</param>
        /// <returns>JSON string.</returns>
        Task<string> ApiCall(string service, string method, string version, string parameters);
    }
}
