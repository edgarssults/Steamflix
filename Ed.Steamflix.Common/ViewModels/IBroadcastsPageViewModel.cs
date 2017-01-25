using Ed.Steamflix.Common.Models;

namespace Ed.Steamflix.Common.ViewModels
{
    public interface IBroadcastsPageViewModel
    {
        /// <summary>
        /// List of broadcasts for a game.
        /// </summary>
        NotifyTaskCompletion<GetBroadcastsResponse> Broadcasts { get; }
    }
}
