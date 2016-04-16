using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Mocks.Repositories;
using System.Collections.Generic;

namespace Ed.Steamflix.Mocks.ViewModels
{
    public class GamesPageViewModelMock : IGamesPageViewModel
    {
        private readonly GameService _playerService = new GameService(new TestApiRepository());

        /// <summary>
        /// User's Steam ID.
        /// </summary>
        public string SteamId
        {
            get
            {
                return "76561197974081377"; // Me
            }
        }

        /// <summary>
        /// Currently selected game, for passing info to other pages.
        /// </summary>
        public Game SelectedGame { get; set; }

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
    }
}
