using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Ed.Steamflix.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Load profile URL and redirect
            var profileUrl = (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"];
            if (!string.IsNullOrEmpty(profileUrl))
            {
                ProfileUrl.Text = profileUrl;
            }

#if DEBUG
            if (string.IsNullOrEmpty(ProfileUrl.Text))
            {
                ProfileUrl.Text = "http://steamcommunity.com/id/edgarssults";
            }
#endif
        }

        private void Start_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            // http://steamcommunity.com/id/edgarssults
            // http://steamcommunity.com/profiles/76561198084024217

            if (!string.IsNullOrEmpty(ProfileUrl.Text))
            {
                // If the profile URL is different, clear the extracted Steam ID
                if (ProfileUrl.Text != (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"])
                {
                    ApplicationData.Current.RoamingSettings.Values["SteamId"] = null;
                }

                // Save new profile URL
                ApplicationData.Current.RoamingSettings.Values["ProfileUrl"] = ProfileUrl.Text;
                ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = false;

                // Navigate to games page
                Frame.Navigate(typeof(GamesPage), ProfileUrl.Text);
            }
            else
            {
                // Not providing a profile URL
                ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = true;
                ApplicationData.Current.RoamingSettings.Values["SteamId"] = null;

                // Navigate to games page
                Frame.Navigate(typeof(GamesPage), null);
            }
        }

        private void ProfileUrl_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Start_Tapped(sender, null);
            }
        }
    }
}