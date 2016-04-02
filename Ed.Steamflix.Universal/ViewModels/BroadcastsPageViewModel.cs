using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using System.Collections.Generic;
using Windows.Storage;

namespace Ed.Steamflix.Universal.ViewModels
{
    public class BroadcastsPageViewModel : IBroadcastsPageViewModel
    {
        // TODO: DI
        private readonly BroadcastService _broadcastService = new BroadcastService(new CommunityRepository());
        private readonly PlayerService _playerService = new PlayerService(new ApiRepository());

        private int _appId;
        private Game _game;

        /// <summary>
        /// Constructor when a game object is available.
        /// </summary>
        /// <param name="game">Game object.</param>
        public BroadcastsPageViewModel(Game game)
        {
            _game = game;
            _appId = game.AppId;
        }

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
                return new NotifyTaskCompletion<List<Broadcast>>(_broadcastService.GetBroadcastsAsync(Game.AppId));
            }
        }

        /// <summary>
        /// The game the broadcasts page is about.
        /// </summary>
        public Game Game
        {
            get
            {
                if (_game == null)
                {
                    // TODO: Not async, blocks UI
                    _game = _playerService.GetGameInfoAsync((string)ApplicationData.Current.RoamingSettings.Values["SteamId"], _appId).Result;
                }

                return _game;
            }
            set
            {
                _game = value;
            }
        }
    }
}
