using Ed.Steamflix.Common;
using Ed.Steamflix.Common.Repositories;
using Ed.Steamflix.Common.Services;
using Microsoft.HockeyApp;
using Ninject;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Ed.Steamflix.Universal
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
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

            // DI kernel
            IKernel kernel = new StandardKernel();
            DependencyHelper.InitNinjectKernel(kernel);

            // Repositories
            kernel.Bind<IApiRepository>().To<ApiRepository>().InSingletonScope();
            kernel.Bind<ICommunityRepository>().To<CommunityRepository>().InSingletonScope();

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
                this.DebugSettings.EnableFrameRateCounter = true;
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
