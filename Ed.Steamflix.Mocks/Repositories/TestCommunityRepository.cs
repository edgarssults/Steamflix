using Ed.Steamflix.Common.Repositories;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Ed.Steamflix.Mocks.Repositories
{
    public class TestCommunityRepository : ICommunityRepository
    {
        private readonly ResourceLoader _rl = ResourceLoader.GetForCurrentView("Ed.Steamflix.Mocks/Resources");

        public Task<string> GetBroadcastHtmlAsync(int appId)
        {
            return Task.Run(() => _rl.GetString("BroadcastsHtml" + appId));
        }

        public Task<string> GetStatsHtmlAsync()
        {
            return Task.Run(() => _rl.GetString("StatsHtml"));
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