using Ed.Steamflix.Universal.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SetUpBackButton();

            // Show the broadcast in the page's browser
            // URL should be passed in navigation parameters
            var watchUrl = (string)e.Parameter;
            Browser.Source = new Uri(watchUrl);

            //Browser.InvokeScript("BroadcastWatch.m_playerUI.ToggleFullscreen()", null); // TODO
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.GoBack(e);
        }
    }
}
