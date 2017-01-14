using Ed.Steamflix.Common.Models;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.ViewModels
{
    public interface ISettingsPaneViewModel
    {
        /// <summary>
        /// User search text.
        /// </summary>
        string SearchText { get; set; }

        /// <summary>
        /// Saved Steam profile name.
        /// </summary>
        string ProfileName { get; set; }

        /// <summary>
        /// List of found users.
        /// </summary>
        NotifyTaskCompletion<List<User>> Users { get; }
    }
}
