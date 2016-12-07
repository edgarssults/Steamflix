using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Mocks.Repositories;
using System.Collections.Generic;

namespace Ed.Steamflix.Mocks.ViewModels
{
    public class GamesPageViewModelMock : IGamesPageViewModel
    {
        private readonly GameService _gameService = new GameService(new TestApiRepository(), new TestCommunityRepository());

        public string GetSteamId()
        {
            return "76561197974081377"; // Me
        }

        public NotifyTaskCompletion<List<Game>> RecentlyPlayedGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_gameService.GetRecentlyPlayedGamesAsync(GetSteamId()));
            }
        }

        public NotifyTaskCompletion<List<Game>> OwnedGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_gameService.GetOwnedGamesAsync(GetSteamId()));
            }
        }

        public NotifyTaskCompletion<List<Game>> PopularGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_gameService.GetPopularGamesAsync());
            }
        }

        public bool SteamIdIsAvailable
        {
            get
            {
                return true;
            }
        }
    }
}
