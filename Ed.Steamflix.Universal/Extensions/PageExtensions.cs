using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Ed.Steamflix.Universal.Extensions
{
    public static class PageExtensions
    {
        /// <summary>
        /// Enables the Windows 10 back button for a page.
        /// </summary>
        /// <param name="page">Page object.</param>
        public static void SetUpBackButton(this Page page)
        {
            var rootFrame = Window.Current.Content as Frame;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = 
                rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        /// <summary>
        /// Handles back navigation event for a page.
        /// </summary>
        /// <param name="page">Page object.</param>
        /// <param name="e">Tapped event arguments.</param>
        public static void GoBack(this Page page, TappedRoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                return;
            }

            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        /// <summary>
        /// Handles back navigation event for a page.
        /// </summary>
        /// <param name="page">Page object.</param>
        /// <param name="e">Back event arguments.</param>
        public static void GoBack(this Page page, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                return;
            }

            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }
    }
}
