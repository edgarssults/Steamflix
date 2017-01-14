using Windows.Storage;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Ed.Steamflix.Universal
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Continue_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var profileUrl = (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"];

            if (!string.IsNullOrEmpty(profileUrl))
            {
                ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = false;

                // Navigate to games page
                Frame.Navigate(typeof(GamesPage), profileUrl);
            }
            else
            {
                ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = true;
                ApplicationData.Current.RoamingSettings.Values["SteamId"] = null;

                // Navigate to games page
                Frame.Navigate(typeof(GamesPage), null);
            }
        }
    }
}