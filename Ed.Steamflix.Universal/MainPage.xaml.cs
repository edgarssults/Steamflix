using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
            if (!string.IsNullOrEmpty(ProfileUrl.Text))
            {
                // Save profile URL
                ApplicationData.Current.RoamingSettings.Values["ProfileUrl"] = ProfileUrl.Text;

                // Navigate to games page
                Frame.Navigate(typeof(GamesPage), ProfileUrl.Text);
            }
        }
    }
}