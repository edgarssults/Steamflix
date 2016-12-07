using Ed.Steamflix.Common;
using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using System.Collections.Generic;

namespace Ed.Steamflix.Universal.ViewModels
{
    public class BroadcastsPageViewModel : IBroadcastsPageViewModel
    {
        private readonly BroadcastService _broadcastService = DependencyHelper.Resolve<BroadcastService>();
        private readonly GameService _playerService = DependencyHelper.Resolve<GameService>();

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
        public NotifyTaskCompletion<List<Broadcast>> Broadcasts
        {
            get
            {
                return new NotifyTaskCompletion<List<Broadcast>>(_broadcastService.GetBroadcastsAsync(_appId));
            }
        }

        /// <summary>
        /// The game the broadcasts page is about.
        /// </summary>
        public NotifyTaskCompletion<Game> Game
        {
            get
            {
                return new NotifyTaskCompletion<Game>(_playerService.GetGameInfoAsync(_appId));
            }
        }
    }
}
