using DryIoc;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Microsoft.HockeyApp;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Ed.Steamflix.Universal
{
    /// <summary>
    /// Provides application-specific behaviour to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            // Dependency Injection
            var container = new Container();
            DependencyHelper.InitContainer(container);

            // Services
            container.Register<BroadcastService, BroadcastService>(Reuse.Singleton);
            container.Register<GameService, GameService>(Reuse.Singleton);
            container.Register<UserService, UserService>(Reuse.Singleton);

            // Repositories
            container.Register<IApiRepository, ApiRepository>(Reuse.Singleton);
            container.Register<ICommunityRepository, CommunityRepository>(Reuse.Singleton);

            // HockeyApp
            HockeyClient.Current.Configure("ba87431fcc8c44d3b3562a9a07e8d58f");
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = false;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter

                // Load profile URL and go to Games page
                var profileUrl = (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"];
                if (!string.IsNullOrEmpty(profileUrl))
                {
                    rootFrame.Navigate(typeof(GamesPage), profileUrl);
                }
                else
                {
                    // Start without Steam ID?
                    var loadWithout = ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] as bool?;
                    if (loadWithout.HasValue && loadWithout.Value)
                    {
                        rootFrame.Navigate(typeof(GamesPage), null);
                    }
                    else
                    {
                        // First time
                        rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    }
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();

            // Tile
            SetupPeriotidcTileUpdate();

            // Full screen on phones
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                var view = ApplicationView.GetForCurrentView();
                if (!view.IsFullScreenMode)
                {
                    view.TryEnterFullScreenMode();
                }
            }
        }

        /// <summary>
        /// Sets up a periodic tile update that shows 5 recently played and popular game images from the Steamflix API.
        /// </summary>
        private void SetupPeriotidcTileUpdate()
        {
            var apiUrl = "http://steamflix.azurewebsites.net/api/tile";
            var steamId = GetSteamId();
            var uris = new List<Uri>
            {
                new Uri($"{apiUrl}/0/{steamId}"),
                new Uri($"{apiUrl}/1/{steamId}"),
                new Uri($"{apiUrl}/2/{steamId}"),
                new Uri($"{apiUrl}/3/{steamId}"),
                new Uri($"{apiUrl}/4/{steamId}")
            };

            var tileUpdateManager = TileUpdateManager.CreateTileUpdaterForApplication();
            tileUpdateManager.EnableNotificationQueue(true);
            tileUpdateManager.StartPeriodicUpdateBatch(uris, PeriodicUpdateRecurrence.TwelveHours);
        }

        /// <summary>
        /// Gets Steam ID from settings.
        /// </summary>
        /// <returns></returns>
        private string GetSteamId()
        {
            // TODO: Move to Utils?

            var steamId = (string)ApplicationData.Current.RoamingSettings.Values["SteamId"];

            if (string.IsNullOrEmpty(steamId))
            {
                var profileUrl = (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"];

                if (!string.IsNullOrEmpty(profileUrl))
                {
                    // Have to extract ID from profile URL
                    steamId = DependencyHelper.Resolve<UserService>().GetSteamIdAsync(profileUrl).Result;

                    // Save ID
                    ApplicationData.Current.RoamingSettings.Values["SteamId"] = steamId;
                }
            }

            return steamId;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity

            deferral.Complete();
        }
    }
}
