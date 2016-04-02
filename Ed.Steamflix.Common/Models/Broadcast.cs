namespace Ed.Steamflix.Common.Models
{
    /// <summary>
    /// Represents a broadcast scraped from a game's community broadcasts page.
    /// </summary>
    public class Broadcast
    {
        /// <summary>
        /// Broadcast watching URL.
        /// </summary>
        public string WatchUrl { get; set; }

        /// <summary>
        /// User that's broadcasting.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// URL of the broadcasts's thumbnail image.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}