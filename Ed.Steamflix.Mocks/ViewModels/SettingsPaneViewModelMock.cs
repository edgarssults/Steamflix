using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Mocks.Repositories;
using System.Collections.Generic;

namespace Ed.Steamflix.Mocks.ViewModels
{
    public class SettingsPaneViewModelMock : ISettingsPaneViewModel
    {
        private readonly UserService _userService = new UserService(new TestApiRepository(), new TestCommunityRepository());

        public string ProfileName { get; set; } = "Eadgar";

        public string SearchText { get; set; } = "eadgar";

        public NotifyTaskCompletion<List<User>> Users
        {
            get
            {
                return new NotifyTaskCompletion<List<User>>(_userService.FindUsersAsync(SearchText));
            }
        }
    }
}
