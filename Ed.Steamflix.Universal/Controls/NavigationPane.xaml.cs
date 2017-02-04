using Microsoft.Services.Store.Engagement;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ed.Steamflix.Universal.Controls
{
    public sealed partial class NavigationPane : UserControl
    {
        public NavigationPane()
        {
            this.InitializeComponent();

            // Show the feedback button if it's supported or if we're in design mode
            bool designMode = Windows.ApplicationModel.DesignMode.DesignModeEnabled;
            if (designMode || StoreServicesFeedbackLauncher.IsSupported())
            {
                this.FeedbackButtonWrapper.Visibility = Visibility.Visible;
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
            NavigationView.Width = NavigationView.IsPaneOpen ? NavigationView.OpenPaneLength : NavigationView.CompactPaneLength;
        }

        private async void RateButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
        }

        private async void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            var launcher = StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }

        private void NavigateToGames(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(GamesPage));
        }

        private void NavigateToSettings(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(SettingsPage));
        }
    }
}