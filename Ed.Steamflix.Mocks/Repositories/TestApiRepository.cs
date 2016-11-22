using Ed.Steamflix.Common.Repositories;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Ed.Steamflix.Mocks.Repositories
{
    public class TestApiRepository : IApiRepository
    {
        private readonly ResourceLoader _rl = ResourceLoader.GetForCurrentView("Ed.Steamflix.Mocks/Resources");

        public Task<string> ApiCallAsync(string service, string method, string version, string parameters)
        {
            return Task.Run(() => _rl.GetString(method + "ResponseJson"));
        }

        public Task<string> ReadUrlAsync(string url)
        {
            return Task.Run(() => _rl.GetString(Regex.Replace(url, @"[\:\/\.\=\?]", "")));
        }
    }
}
