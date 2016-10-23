using Ed.Steamflix.Universal.Extensions;
using System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ed.Steamflix.Universal
{
    public sealed partial class WatchPage : Page
    {
        public WatchPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            this.GoBack(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the broadcast playback before navigating away
            Browser.NavigateToString("");

            // Listen for full screen exit event
            Window.Current.SizeChanged -= OnWindowResize;

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SetUpBackButton();

            // Show the broadcast in the page's browser
            // URL should be passed in navigation parameters
            var watchUrl = (string)e.Parameter;
            Browser.Source = new Uri(watchUrl);

            // Stop listening for full screen exit event
            Window.Current.SizeChanged += OnWindowResize;

            // Show/hide the command bar
            UpdateContent();
        }

        private void AppBarBackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.GoBack(e);
        }

        private void AppBarFullScreenButton_Tapped(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();

            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
                WatchCommandBar.Visibility = Visibility.Visible;
            }
            else
            {
                view.TryEnterFullScreenMode();
                WatchCommandBar.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Handles window resizing events, such as exiting from full screen mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnWindowResize(object sender, WindowSizeChangedEventArgs e)
        {
            UpdateContent();
        }

        /// <summary>
        /// Shows/hides the command bar depending on full screen mode status.
        /// </summary>
        void UpdateContent()
        {
            var view = ApplicationView.GetForCurrentView();
            WatchCommandBar.Visibility = view.IsFullScreenMode ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
