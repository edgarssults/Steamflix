using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Universal.Extensions;
using Ed.Steamflix.Universal.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.System.Profile;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BroadcastsPage : Page
    {
        public IBroadcastsPageViewModel ViewModel { get; set; }

        public BroadcastsPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            this.GoBack(e);
        }

        private void BroadcastListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var broadcast = (e.OriginalSource as FrameworkElement).DataContext as Broadcast;
            Frame.Navigate(typeof(WatchPage), broadcast.WatchUrl);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SetUpBackButton();

            // If no view model, set it up
            if (ViewModel == null)
            {
                ViewModel = new BroadcastsPageViewModel((int)e.Parameter);
            }

            BroadcastsHub.DataContext = ViewModel;
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.GoBack(e);
        }
    }
}
