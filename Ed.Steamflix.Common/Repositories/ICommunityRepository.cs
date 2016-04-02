using System.IO;
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
    }
}
