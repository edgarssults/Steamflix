using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ed.Steamflix.Universal
{
    public sealed partial class MobileNavigationPane : UserControl
    {
        public MobileNavigationPane()
        {
            this.InitializeComponent();
        }

        private void Home_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(GamesPage));
        }

        private void Settings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(SettingsPage));
        }

        // TODO: This control should have a view model with the buttons that should be displayed, it will be different for each page
    }
}
