using Ed.Steamflix.Universal.Extensions;
using System;
using Windows.System;
using Windows.System.Profile;
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
            Browser.DOMContentLoaded += Browser_HideElements;
        }

        /// <summary>
        /// Hides elements we don't care about on the broadcast page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void Browser_HideElements(object sender, WebViewDOMContentLoadedEventArgs args)
        {
            try
            {
                // Hiding a bunch of elements like menus, header, chat, footer, content, built-in full screen button
                var elementsToHide = "#global_header, #footer_spacer, #footer_responsive_optin_spacer, #footer, #ChatWindow, .BroadcastInfoWrapper, .fullscreen_button, .responsive_header";

                // Hide HTML elements and change styles so it looks like the broadcast is in full-screen
                // We can't call the broadcast's own toggle full-screen function
                var lines = new[]
                {
                    "document.body.style.overflow = 'hidden';", // Get rid of scrollbars (there shouldn't be a need for any)
                    "document.getElementById('video_wrapper').className += ' fullscreen';", // Switch video to full screen mode
                    "document.getElementById('video_content').style.padding = 0;", // Remove video padding
                    "document.getElementById('video_content').style.margin = 0;", // Remove video margin
                    "document.getElementsByClassName('pagecontent')[0].style.padding = 0;", // Remove content padding
                    "document.getElementsByClassName('responsive_page_content')[0].style.paddingTop = 0;", // Remove small screen content padding
                    "var list = document.querySelectorAll('" + elementsToHide + "');", // Find all elements in the list
                    "for (var i = 0; i < list.length; i++) { var e = list[i]; e.style.display = 'none'; }", // Hide all found elements
                    "if (document.getElementById('PageLoadingText').innerHTML.indexOf('Touch to start') > -1) { AndroidClickStart(); }" // Start playing the video on small screens
                };
                await Browser.InvokeScriptAsync("eval", new[] { string.Join(" ", lines) });
            }
            catch
            {
                // Ignore script exceptions
            }
        }

        /// <summary>
        /// Handles back button press.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            this.GoBack(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the broadcast playback before navigating away
            Browser.NavigateToString("");

            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                // Mobiles just show the command bar
                if (WatchCommandBar.Visibility == Visibility.Collapsed)
                {
                    WatchCommandBar.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // All others exit full screen and show the command bar
                var view = ApplicationView.GetForCurrentView();
                if (view.IsFullScreenMode)
                {
                    view.ExitFullScreenMode();
                    WatchCommandBar.Visibility = Visibility.Visible;
                }
            }

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SetUpBackButton();

            // Show the broadcast in the page's browser
            // URL should be passed in navigation parameters
            var watchUrl = (string)e.Parameter;
            Browser.Source = new Uri(watchUrl);
        }

        /// <summary>
        /// Navigates back to the previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarBackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.GoBack(e);
        }

        /// <summary>
        /// Switches the app to full screen depending on device family.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarFullScreenButton_Tapped(object sender, RoutedEventArgs e)
        {
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                // Mobiles show/hide the command bar
                if (WatchCommandBar.Visibility == Visibility.Collapsed)
                {
                    WatchCommandBar.Visibility = Visibility.Visible;
                }
                else
                {
                    WatchCommandBar.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                // All others enter/exit full screen and show/hide the command bar
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
        }

        /// <summary>
        /// Opens the broadcast URL in the browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AppBarBrowserButton_Tapped(object sender, RoutedEventArgs e)
        {
            // Pause playback
            try
            {
                await Browser.InvokeScriptAsync("eval", new[] { "document.getElementsByClassName('play_button')[0].click();" });
            }
            catch
            {
                // Ignore script exceptions
            }

            // Launch browser
            await Launcher.LaunchUriAsync(Browser.Source);
        }
    }
}
