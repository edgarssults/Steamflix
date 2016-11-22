using Ed.Steamflix.Common.ViewModels;
using Windows.Storage;

namespace Ed.Steamflix.Universal.ViewModels
{
    public class SettingsPaneViewModel : ISettingsPaneViewModel
    {
        public SettingsPaneViewModel() { }

        /// <summary>
        /// User's Steam community profile URL.
        /// </summary>
        public string ProfileUrl
        {
            get
            {
                return (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"] ?? string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // If the profile URL is different, clear the extracted Steam ID
                    if (value != (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"])
                    {
                        ApplicationData.Current.RoamingSettings.Values["SteamId"] = null;
                    }

                    // Save new profile URL
                    ApplicationData.Current.RoamingSettings.Values["ProfileUrl"] = value;
                    ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = false;
                }
                else
                {
                    // Clearing saved values
                    ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = true;
                    ApplicationData.Current.RoamingSettings.Values["SteamId"] = null;
                    ApplicationData.Current.RoamingSettings.Values["ProfileUrl"] = null;
                }
            }
        }
    }
}
