using Ed.Steamflix.Common.Repositories;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ed.Steamflix.Mocks.Repositories
{
    public class TestApiRepository : IApiRepository
    {
        public Task<string> ApiCallAsync(string service, string method, string version, string parameters)
        {
            return Task.Run(() => Resources.ResourceManager.GetString(method + "ResponseJson"));
        }

        public Task<string> ReadUrlAsync(string url)
        {
            return Task.Run(() => Resources.ResourceManager.GetString(Regex.Replace(url, @"[\:\/\.\=\?]", "")));
        }
    }
}
