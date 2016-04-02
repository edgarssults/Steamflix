using Ed.Steamflix.Common.Models;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.ViewModels
{
    public interface IGamesPageViewModel
    {
        /// <summary>
        /// User's Steam ID.
        /// </summary>
        string SteamId { get; }

        /// <summary>
        /// Currently selected game, for passing info to other pages.
        /// </summary>
        Game SelectedGame { get; set; }

        /// <summary>
        /// List of recently played games.
        /// </summary>
        NotifyTaskCompletion<List<Game>> RecentlyPlayedGames { get; }

        /// <summary>
        /// List of owned games.
        /// </summary>
        NotifyTaskCompletion<List<Game>> OwnedGames { get; }
    }
}