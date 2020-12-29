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
            Browser.DOMContentLoaded += OnBrowserLoaded;
            SizeChanged += OnSizeChanged;
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
        /// Handles page size change event.
        /// </summary>
        /// <remarks>
        /// Shows the command bar if necessary.
        /// </remarks>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Width == 0 && e.PreviousSize.Height == 0)
            {
                // Fresh window, no change
                return;
            }

            var view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode)
            {
                // Size changed because full screen mode was exited or the window was resized
                // Either way, command bar should be visible
                WatchCommandBar.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Handles browser DOM loaded event.
        /// </summary>
        /// <remarks>
        /// Hides elements we don't care about.
        /// </remarks>
        private async void OnBrowserLoaded(object sender, WebViewDOMContentLoadedEventArgs args)
        {
            try
            {
                // Hiding a bunch of elements like menus, header, chat, footer, content, built-in full screen button
                var elementsToHide = "#global_header, #footer_spacer, #footer_responsive_optin_spacer, #footer";

                // Hide HTML elements and change styles so it looks like the broadcast is in full-screen
                // We can't call the broadcast's own toggle full-screen function
                var lines = new[]
                {
                    "var checkVideoExists = setInterval(function() {", // Have to wait for the video to load
                    "if (document.getElementsByClassName('videoSrc').length) {",
                    "clearInterval(checkVideoExists);",
                    "$J('[class^=broadcast_embeddable_detail_chat_ctn]').hide();", // Hide chat
                    "$J('[class^=broadcast_embeddable_video_placeholder]').css('height', '100vh');", // Full video height
                    "$J('[class^=broadcast_embeddable_video_placeholder]').css('padding-right', '0');", // Remove video padding
                    "var elementList = document.querySelectorAll('" + elementsToHide + "');", // Find all elements in the list
                    "for (var i = 0; i < elementList.length; i++) { var e = elementList[i]; e.style.display = 'none'; }", // Hide elements
                    "var checkFullscreenExists = setInterval(function() { if ($J('.BroadcastFullscreenToggle').is(':visible')) { $J('.BroadcastFullscreenToggle').hide(); }}, 200);", // Hide fullscreen button when it becomes visible
                    "}}, 100);",
                    "var checkProfileExists = setInterval(function() { if ($J('[class^=broadcastprofile_ProfileCtn]').length) { clearInterval(checkProfileExists); $J('[class^=broadcastprofile_ProfileCtn]').hide(); }}, 100);",
                    "var checkInfoExists = setInterval(function() { if ($J('[class^=broadcastplayer_GameInfoCtn]').length) { clearInterval(checkInfoExists); $J('[class^=broadcastplayer_GameInfoCtn]').hide(); }}, 100);",
                    "var checkEventsExist = setInterval(function() { if ($J('[class^=broadcastplayer_RelatedEvents]').length) { clearInterval(checkEventsExist); $J('[class^=broadcastplayer_RelatedEvents]').hide(); }}, 100);",
                    "setTimeout(() => { clearInterval(checkVideoExists); clearInterval(checkProfileExists); clearInterval(checkInfoExists); clearInterval(checkEventsExist); }, 10000);", // Clear all intervals after a while
                };
                await Browser.InvokeScriptAsync("eval", new string[] { string.Join(" ", lines) });
            }
            catch
            {
                // Ignore script exceptions
            }
        }

        /// <summary>
        /// Handles back button tap event from app.
        /// </summary>
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            this.GoBack(e);
        }

        /// <summary>
        /// Handles back button tap event from command bar.
        /// </summary>
        /// <remarks>
        /// Navigates back to the previous page.
        /// </remarks>
        private void AppBarBackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.GoBack(e);
        }

        /// <summary>
        /// Handles full screen button tap event.
        /// </summary>
        /// <remarks>
        /// Switches the app to full screen depending on device family.
        /// </remarks>
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
        /// Handles browser button tap event.
        /// </summary>
        /// <remarks>
        /// Opens the broadcast URL in the browser.
        /// </remarks>
        private async void AppBarBrowserButton_Tapped(object sender, RoutedEventArgs e)
        {
            // Can't pause playback because pause button is not visible
            // Launch browser
            await Launcher.LaunchUriAsync(Browser.Source);
        }
    }
}
