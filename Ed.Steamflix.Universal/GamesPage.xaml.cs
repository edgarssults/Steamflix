using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Universal.Extensions;
using Ed.Steamflix.Universal.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ed.Steamflix.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamesPage : Page
    {
        public IGamesPageViewModel ViewModel { get; set; }

        public GamesPage()
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
            // Set up the BroadcastsPageViewModel because we have the game info
            if (e.Content is BroadcastsPage)
            {
                var page = e.Content as BroadcastsPage;
                if (page != null)
                {
                    page.ViewModel = new BroadcastsPageViewModel(ViewModel.SelectedGame);
                }
            }

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SetUpBackButton();

            // If no view model, set it up
            if (ViewModel == null)
            {
                ViewModel = new GamesPageViewModel((string)e.Parameter);
            }

            GamesHub.DataContext = ViewModel;
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.GoBack(e);
        }

        private void Game_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Get the game object from the context
            var game = (e.OriginalSource as FrameworkElement).DataContext as Game;

            if (game != null)
            {
                // Change the currently selected game
                ViewModel.SelectedGame = game;

                // Navigate to broadcasts page
                Frame.Navigate(typeof(BroadcastsPage), game.AppId);
            }
        }
    }
}