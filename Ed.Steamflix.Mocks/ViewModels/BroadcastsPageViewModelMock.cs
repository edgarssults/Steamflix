using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Mocks.Repositories;

namespace Ed.Steamflix.Mocks.ViewModels
{
    public class BroadcastsPageViewModelMock : IBroadcastsPageViewModel
    {
        private readonly BroadcastService _broadcastService = new BroadcastService(new TestCommunityRepository());
        private int _appId = 292030;

        public NotifyTaskCompletion<GetBroadcastsResponse> Broadcasts
        {
            get
            {
                return new NotifyTaskCompletion<GetBroadcastsResponse>(_broadcastService.GetBroadcasts(_appId));
            }
        }
    }
}
