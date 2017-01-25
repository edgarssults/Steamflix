using System.Collections.Generic;

namespace Ed.Steamflix.Common.Models
{
    public class GetBroadcastsResponse
    {
        /// <summary>
        /// Name of the game.
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// List of broadcasts available for the game.
        /// </summary>
        public List<Broadcast> Broadcasts { get; set; }
    }
}
