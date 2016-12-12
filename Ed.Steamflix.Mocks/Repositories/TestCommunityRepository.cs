using Ed.Steamflix.Common.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

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
            return Task.Run(() => string.Format(Resources.ResourceManager.GetString("UserSearchUrlFormat"), user));
        }

        public Task<string> GetUsersHtmlAsync(string user, string sessionId, string steamCountry)
        {
            return Task.Run(() => Resources.ResourceManager.GetString("UserSearchResponse"));
        }
        
        public Task<List<Cookie>> GetSteamSetCookiesAsync()
        {
            var uri = new System.Uri("http://steamcommunity.com");
            var cookies = new CookieContainer();
            cookies.SetCookies(uri, Resources.SteamCookies);
            return Task.Run(() => cookies.GetCookies(uri).Cast<Cookie>().ToList());
        }
    }
}