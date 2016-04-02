using Ed.Steamflix.Common.Repositories;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Ed.Steamflix.Mocks.Repositories
{
    public class TestCommunityRepository : ICommunityRepository
    {
        private readonly ResourceLoader _rl = new ResourceLoader("Ed.Steamflix.Mocks/Resources");

        public Task<string> GetBroadcastHtmlAsync(int appId)
        {
            // Gets the method response from test resources
            return Task.Run(() => _rl.GetString("BroadcastsHtml" + appId));
        }
    }
}