using System;

namespace Ed.Steamflix.Common
{
    public static class ApiHelper
    {
        /// <summary>
        /// Replace this with a real API key.
        /// </summary>
        private static readonly string _steamApiKey = "TEMP";

        /// <summary>
        /// The Steam API key to be used with all API requests.
        /// </summary>
        public static string SteamApiKey
        {
            get
            {
                if (_steamApiKey.Equals("TEMP"))
                {
                    throw new Exception("Please provide a valid Steam API key!");
                }

                return _steamApiKey;
            }
        }
    }
}
