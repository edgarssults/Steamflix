using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Mocks.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ed.Steamflix.Mocks.ViewModels
{
    public class BroadcastsPageViewModelMock : IBroadcastsPageViewModel
    {
        private readonly BroadcastService _broadcastService = new BroadcastService(new TestCommunityRepository());
        private int _appId = 292030;

        public NotifyTaskCompletion<List<Broadcast>> Broadcasts
        {
            get
            {
                return new NotifyTaskCompletion<List<Broadcast>>(_broadcastService.GetBroadcastsAsync(_appId));
            }
        }

        public NotifyTaskCompletion<Game> Game
        {
            get
            {
                return new NotifyTaskCompletion<Game>(new Task<Game>(() => {
                    return new Game
                    {
                        Name = "Witcher 3",
                        AppId = _appId
                    };
                }));
            }
        }
    }
}
