using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Mocks.Repositories;
using System.Collections.Generic;
using System;

namespace Ed.Steamflix.Mocks.ViewModels
{
    public class GamesPageViewModelMock : IGamesPageViewModel
    {
        private readonly GameService _playerService = new GameService(new TestApiRepository(), new TestCommunityRepository());

        public string SteamId
        {
            get
            {
                return "76561197974081377"; // Me
            }
        }

        public NotifyTaskCompletion<List<Game>> RecentlyPlayedGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_playerService.GetRecentlyPlayedGamesAsync(SteamId));
            }
        }

        public NotifyTaskCompletion<List<Game>> OwnedGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_playerService.GetOwnedGamesAsync(SteamId));
            }
        }

        public NotifyTaskCompletion<List<Game>> PopularGames
        {
            get
            {
                return new NotifyTaskCompletion<List<Game>>(_playerService.GetPopularGamesAsync());
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
