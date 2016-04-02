using Windows.Storage;
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

            // Load profile URL
            var profileUrl = (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"];
            if (!string.IsNullOrEmpty(profileUrl))
            {
                ProfileUrl.Text = profileUrl;
            }
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

                // Navigate to games page
                Frame.Navigate(typeof(GamesPage), ProfileUrl.Text);
            }
        }
    }
}