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
        /// List of recently played games.
        /// </summary>
        NotifyTaskCompletion<List<Game>> RecentlyPlayedGames { get; }

        /// <summary>
        /// List of owned games.
        /// </summary>
        NotifyTaskCompletion<List<Game>> OwnedGames { get; }

        /// <summary>
        /// List of popular games.
        /// </summary>
        NotifyTaskCompletion<List<Game>> PopularGames { get; }
    }
}