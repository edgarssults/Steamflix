using Ed.Steamflix.Universal.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ed.Steamflix.Universal
{
    public sealed partial class SettingsPane : UserControl
    {
        public SettingsPaneViewModel ViewModel { get; set; }

        public SettingsPane()
        {
            this.InitializeComponent();

            if (ViewModel == null)
            {
                ViewModel = new SettingsPaneViewModel();
            }

            this.DataContext = ViewModel;
        }

        private void Save_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.ProfileUrl = SettingsProfileUrl.Text;
        }

        private void SettingsProfileUrl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Save_Tapped(sender, null);
            }
        }
    }
}
