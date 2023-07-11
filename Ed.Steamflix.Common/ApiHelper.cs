using System;

namespace Ed.Steamflix.Common
{
    public static class ApiHelper
    {
        /// <summary>
        /// Replace this with a real API key.
        /// </summary>
        /// <remarks>
        /// 
        /// To prevent it from being accidentally checked in, open the Git Bash in the root of this project and execute the following command:
        /// git update-index --assume-unchanged ApiHelper.cs
        /// 
        /// To revert it, execute the following:
        /// git update-index --no-assume-unchanged ApiHelper.cs
        /// 
        /// From http://stackoverflow.com/questions/3319479/git-can-i-commit-a-file-and-ignore-the-content-changes
        /// 
        /// </remarks>
        private static readonly string _steamApiKey = "8FAC8288FB26E59C1468DAD0DFED2683";

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
