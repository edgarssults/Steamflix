using Ed.Steamflix.Common.Repositories;
using System.Threading.Tasks;

namespace Ed.Steamflix.Mocks.Repositories
{
    public class TestCommunityRepository : ICommunityRepository
    {
        public Task<string> GetBroadcastHtmlAsync(int appId)
        {
            return Task.Run(() => Resources.ResourceManager.GetString("BroadcastsHtml" + appId));
        }

        public Task<string> GetStatsHtmlAsync()
        {
            return Task.Run(() => Resources.ResourceManager.GetString("StatsHtml"));
        }

        public Task<string> GetUserSearchHtmlAsync(string user)
        {
            return Task.Run(() => string.Format(_rl.GetString("UserSearchUrlFormat"), user));
        }
        public Task<string> FindUsersAsync(string user, string sessionId, string steamCountry)
        {
            return Task.Run(() => _rl.GetString("UserSearchResponse"));
        }
        
        public Task<string> GetSteamSetCookiesAsync()
        {
            return Task.Run(() => _rl.GetString("SteamCookies"));
        }
    }
}