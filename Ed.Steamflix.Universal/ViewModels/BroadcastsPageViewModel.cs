using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;

namespace Ed.Steamflix.Universal.ViewModels
{
    public class BroadcastsPageViewModel : IBroadcastsPageViewModel
    {
        private readonly BroadcastService _broadcastService = DependencyHelper.Resolve<BroadcastService>();
        private readonly GameService _gameService = DependencyHelper.Resolve<GameService>();

        private int _appId;

        public BroadcastsPageViewModel() { }

        /// <summary>
        /// Constructor when only an app identifier is available.
        /// </summary>
        /// <param name="appId">App identifier.</param>
        public BroadcastsPageViewModel(int appId)
        {
            _appId = appId;
        }

        /// <summary>
        /// List of broadcasts for a game.
        /// </summary>
        public NotifyTaskCompletion<GetBroadcastsResponse> Broadcasts
        {
            get
            {
                return new NotifyTaskCompletion<GetBroadcastsResponse>(_broadcastService.GetBroadcastsAsync(_appId));
            }
        }
    }
}
