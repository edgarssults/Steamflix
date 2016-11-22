using Ed.Steamflix.Common.ViewModels;

namespace Ed.Steamflix.Mocks.ViewModels
{
    public class SettingsPaneViewModelMock : ISettingsPaneViewModel
    {
        public string ProfileUrl { get; set; } = "http://steamcommunity.com/id/edgarssults";
    }
}
