using Ed.Steamflix.Common.Models;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.ViewModels
{
    public interface IBroadcastsPageViewModel
    {
        /// <summary>
        /// List of broadcasts for a game.
        /// </summary>
        NotifyTaskCompletion<List<Broadcast>> Broadcasts { get; }

        /// <summary>
        /// The game the broadcasts page is about.
        /// </summary>
        NotifyTaskCompletion<Game> Game { get; }
    }
}
