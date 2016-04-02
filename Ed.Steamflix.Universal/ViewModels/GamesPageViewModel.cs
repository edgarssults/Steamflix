using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using System.Collections.Generic;
using Windows.Storage;

namespace Ed.Steamflix.Universal.ViewModels
{
    public class GamesPageViewModel : IGamesPageViewModel
    {
        // TODO: DI
        private readonly PlayerService _playerService = new PlayerService(new ApiRepository());
        private readonly SteamUser _steamUser = new SteamUser(new ApiRepository());

        private string _profileUrl;

        public GamesPageViewModel(string profileUrl)
        {
            _profileUrl = profileUrl;
        }

        /// <summary>
        /// User's Steam ID.
        /// </summary>
        public string SteamId
        {
            get
            {
                // Try to get ID from settings
                var steamId = (string)ApplicationData.Current.RoamingSettings.Values["SteamId"];

                if (string.IsNullOrEmpty(steamId))
                {
                    // Have to extract ID from profile URL
                    // TODO: Not async, blocks UI
                    steamId = _steamUser.GetSteamIdAsync(_profileUrl).Result;

                    // Save ID
                    ApplicationData.Current.RoamingSettings.Values["SteamId"] = steamId;
                }

                return steamId;
            }
        }

        /// <summary>
        /// List of recently played games.
        /// </summary>
        public NotifyTaskCompletion<List<Game>> RecentlyPlayedGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_playerService.GetRecentlyPlayedGamesAsync(SteamId));
            }
        }

        /// <summary>
        /// List of owned games.
        /// </summary>
        public NotifyTaskCompletion<List<Game>> OwnedGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_playerService.GetOwnedGamesAsync(SteamId));
            }
        }

        /// <summary>
        /// Currently selected game, for passing info to other pages.
        /// </summary>
        public Game SelectedGame { get; set; }
    }
}
