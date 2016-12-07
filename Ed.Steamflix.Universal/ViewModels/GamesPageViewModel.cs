using Ed.Steamflix.Common;
using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace Ed.Steamflix.Universal.ViewModels
{
    public class GamesPageViewModel : IGamesPageViewModel
    {
        private readonly GameService _playerService = DependencyHelper.Resolve<GameService>();
        private readonly UserService _steamUser = DependencyHelper.Resolve<UserService>();

        public GamesPageViewModel() { }

        /// <summary>
        /// Whether Steam ID was provided by the user. used to show/hide hub sections.
        /// </summary>
        public bool SteamIdIsAvailable
        {
            get
            {
                return !string.IsNullOrEmpty(GetSteamId());
            }
        }

        /// <summary>
        /// User's Steam ID.
        /// </summary>
        public string GetSteamId()
        {
            var steamId = (string)ApplicationData.Current.RoamingSettings.Values["SteamId"];

            if (string.IsNullOrEmpty(steamId))
            {
                var profileUrl = (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"];

                if (!string.IsNullOrEmpty(profileUrl))
                {
                    // Have to extract ID from profile URL
                    steamId = _steamUser.GetSteamIdAsync(profileUrl).Result;

                    // Save ID
                    ApplicationData.Current.RoamingSettings.Values["SteamId"] = steamId;
                }
            }

            return steamId;
        }

        /// <summary>
        /// List of recently played games.
        /// </summary>
        public NotifyTaskCompletion<List<Game>> RecentlyPlayedGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_playerService.GetRecentlyPlayedGamesAsync(GetSteamId()));
            }
        }

        /// <summary>
        /// List of owned games.
        /// </summary>
        public NotifyTaskCompletion<List<Game>> OwnedGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_playerService.GetOwnedGamesAsync(GetSteamId()));
            }
        }

        /// <summary>
        /// List of popular games.
        /// </summary>
        public NotifyTaskCompletion<List<Game>> PopularGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_playerService.GetPopularGamesAsync());
            }
        }
    }
}
