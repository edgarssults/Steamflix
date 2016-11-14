using Ed.Steamflix.Universal.Extensions;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ed.Steamflix.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            // Load profile URL 
            var profileUrl = (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"];
            if (!string.IsNullOrEmpty(profileUrl))
            {
                SettingsProfileUrl.Text = profileUrl;
            }

            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            this.GoBack(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SetUpBackButton();
        }

        private void Save_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SettingsProfileUrl.Text))
            {
                // If the profile URL is different, clear the extracted Steam ID
                if (SettingsProfileUrl.Text != (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"])
                {
                    ApplicationData.Current.RoamingSettings.Values["SteamId"] = null;
                }

                // Save new profile URL
                ApplicationData.Current.RoamingSettings.Values["ProfileUrl"] = SettingsProfileUrl.Text;
                ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = false;
            }
            else
            {
                // Clearing saved values
                ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = true;
                ApplicationData.Current.RoamingSettings.Values["SteamId"] = null;
                ApplicationData.Current.RoamingSettings.Values["ProfileUrl"] = null;
            }
        }

        private DependencyObject FindChildControl<T>(DependencyObject control, string ctrlName) where T : DependencyObject
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                FrameworkElement fe = child as FrameworkElement;
                // Not a framework element or is null
                if (fe == null) return null;

                if (child is T && fe.Name == ctrlName)
                {
                    // Found the control so return
                    return child;
                }
                else
                {
                    // Not found it - search children
                    DependencyObject nextLevel = FindChildControl<T>(child, ctrlName);
                    if (nextLevel != null)
                    {
                        return nextLevel;
                    }
                }
            }
            return null;
        }
    }
}
