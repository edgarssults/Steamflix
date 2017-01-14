namespace Ed.Steamflix.Common.Models
{
    public class User
    {
        /// <summary>
        /// User's profile name.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// User's real name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Profile URL.
        /// </summary>
        public string ProfileUrl { get; set; }

        /// <summary>
        /// Avatar image URL.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// User's location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Country flag URL.
        /// </summary>
        public string LocationImageUrl { get; set; }
    }
}
