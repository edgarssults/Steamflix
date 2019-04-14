using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Storage;

namespace Ed.Steamflix.Universal.ViewModels
{
    public class SettingsPaneViewModel : ISettingsPaneViewModel, INotifyPropertyChanged
    {
        private readonly UserService _userService = DependencyHelper.Resolve<UserService>();

        public SettingsPaneViewModel() { }

        /// <summary>
        /// User search text.
        /// </summary>
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        private string _searchText;

        /// <summary>
        /// Saved Steam profile name.
        /// </summary>
        public string ProfileName
        {
            get
            {
                return (string)ApplicationData.Current.RoamingSettings.Values["ProfileName"] ?? string.Empty;
            }
            set
            {
                ApplicationData.Current.RoamingSettings.Values["ProfileName"] = value;
                OnPropertyChanged(nameof(ProfileName));
            }
        }

        /// <summary>
        /// List of found users.
        /// </summary>
        public NotifyTaskCompletion<List<User>> Users
        {
            get
            {
                return new NotifyTaskCompletion<List<User>>(_userService.FindUsers(SearchText));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

                // If search text changed, user list also has to be refreshed
                if (propertyName.Equals(nameof(SearchText)))
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Users)));
                }
            }
        }
    }
}
