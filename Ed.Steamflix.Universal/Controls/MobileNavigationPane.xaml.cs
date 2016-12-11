using Microsoft.Services.Store.Engagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System;
using Windows.System;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ed.Steamflix.Universal
{
    public sealed partial class MobileNavigationPane : UserControl
    {
        public MobileNavigationPane()
        {
            this.InitializeComponent();

            // Show the feedback button if it's supported
            if (StoreServicesFeedbackLauncher.IsSupported())
            {
                this.Feedback.Visibility = Visibility.Visible;
            }
        }

        private void Home_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(GamesPage));
        }

        private void Settings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(SettingsPage));
        }

        private async void Rate_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
        }

        private async void Feedback_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var launcher = StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }

        // TODO: This control should have a view model with the buttons that should be displayed, it will be different for each page
    }
}
