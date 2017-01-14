using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Mocks.ViewModels;
using Ed.Steamflix.Universal.ViewModels;
using Windows.ApplicationModel.Store.LicenseManagement;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ed.Steamflix.Universal.Controls
{
    public sealed partial class SettingsPane : UserControl
    {
        public ISettingsPaneViewModel ViewModel { get; set; }
        public object LicenseUsageMode { get; private set; }

        public SettingsPane()
        {
            this.InitializeComponent();

            if (ViewModel == null)
            {
                bool designMode = Windows.ApplicationModel.DesignMode.DesignModeEnabled;
                if (designMode)
                {
                    ViewModel = new SettingsPaneViewModelMock();
                }
                else
                {
                    ViewModel = new SettingsPaneViewModel();
                }
            }

            this.DataContext = ViewModel;
        }

        private void UserList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var user = (e.OriginalSource as FrameworkElement).DataContext as User;
            if (user != null)
            {
                // If the profile URL is different, clear the extracted Steam ID
                if (user.ProfileUrl != (string)ApplicationData.Current.RoamingSettings.Values["ProfileUrl"])
                {
                    ApplicationData.Current.RoamingSettings.Values["SteamId"] = null;
                }

                // Save new profile data
                ApplicationData.Current.RoamingSettings.Values["ProfileUrl"] = user.ProfileUrl;
                ApplicationData.Current.RoamingSettings.Values["StartWithoutSteamId"] = false;
                ViewModel.ProfileName = user.ProfileName;

                // Clear search box
                this.SearchText.Text = string.Empty;
            }
        }
    }
}