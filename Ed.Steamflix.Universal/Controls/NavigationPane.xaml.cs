using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ed.Steamflix.Universal
{
    public sealed partial class NavigationPane : UserControl
    {
        public NavigationPane()
        {
            this.InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
            NavigationView.Width = NavigationView.IsPaneOpen ? NavigationView.OpenPaneLength : NavigationView.CompactPaneLength;
        }

        private void NavigateToGames(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(GamesPage));
        }
    }
}