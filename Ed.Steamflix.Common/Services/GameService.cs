using Newtonsoft.Json;
using Ed.Steamflix.Common.Models;
using System.Threading.Tasks;
using Ed.Steamflix.Common.Repositories;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace Ed.Steamflix.Common.Services
{
    /// <summary>
    /// Service class for Steam's IPlayerService API.
    /// </summary>
    public class GameService
    {
        private readonly Regex _statsRegex = new Regex(@"<tr[^>]*class=""player_count_row"".*?<a[^>]*class=""gameLink""[^>]*href=""[^""]*app/(?<AppId>[^""/]*)[^>]*>(?<Name>[^<]*)</a>.*?</tr>", RegexOptions.Singleline);
        private readonly string _serviceName = "IPlayerService";

        private readonly IApiRepository _apiRepository;
        private readonly ICommunityRepository _communityRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="apiRepository">API repository implementation.</param>
        /// <param name="communityRepository">Community repository implementation.</param>
        public GameService(IApiRepository apiRepository, ICommunityRepository communityRepository)
        {
            _apiRepository = apiRepository;
            _communityRepository = communityRepository;
        }

        /// <summary>
        /// Retrieves the list of recently played games for a user.
        /// </summary>
        /// <remarks>
        /// https://wiki.teamfortress.com/wiki/WebAPI/GetRecentlyPlayedGames
        /// </remarks>
        /// <param name="steamId">User's Steam ID.</param>
        /// <returns>Total count and list of games.</returns>
        public async Task<List<Game>> GetRecentlyPlayedGamesAsync(string steamId)
        {
            if (string.IsNullOrEmpty(steamId))
            {
                return null;
            }

            var call = _apiRepository.ApiCallAsync(
                _serviceName,
                "GetRecentlyPlayedGames",
                "v1",
                $"steamid={steamId}"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<GetRecentlyPlayedGamesResponse>(await call);

            return model.RecentlyPlayedGames.Games;
        }

        /// <summary>
        /// Retrieves a list of owned games for a user.
        /// </summary>
        /// <remarks>
        /// https://wiki.teamfortress.com/wiki/WebAPI/GetOwnedGames
        /// </remarks>
        /// <param name="steamId">User's Steam ID.</param>
        /// <returns>Total count and list of games.</returns>
        public async Task<List<Game>> GetOwnedGamesAsync(string steamId)
        {
            if (string.IsNullOrEmpty(steamId))
            {
                return null;
            }

            var call = _apiRepository.ApiCallAsync(
                _serviceName,
                "GetOwnedGames",
                "v1",
                $"steamid={steamId}&include_appinfo=1"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<GetOwnedGamesResponse>(await call);

            // Re-order the game list
            if (model.OwnedGames.Games != null
                && model.OwnedGames.Games.Any())
            {
                model.OwnedGames.Games = model.OwnedGames.Games.OrderByDescending(g => g.PlaytimeForever).ToList();
            }

            return model.OwnedGames.Games;
        }

        /// <summary>
        /// Retrieves a list of popular games on Steam.
        /// </summary>
        /// <remarks>
        /// http://store.steampowered.com/stats/
        /// </remarks>
        /// <returns>List of games.</returns>
        public async Task<List<Game>> GetPopularGamesAsync()
        {
            var games = new List<Game>();
            var html = await _communityRepository.GetStatsHtmlAsync().ConfigureAwait(false);

            foreach (Match match in _statsRegex.Matches(html))
            {
                games.Add(new Game
                {
                    AppId = int.Parse(match.Groups["AppId"].Value),
                    Name = match.Groups["Name"].Value
                });
            }

            return games;
        }

        /// <summary>
        /// Retrieves info for one game.
        /// </summary>
        /// <remarks>
        /// http://store.steampowered.com/api/appdetails?appids=570
        /// </remarks>
        /// <param name="appId">Steam app identifier.</param>
        /// <returns>Game info.</returns>
        public async Task<Game> GetGameInfoAsync(int appId)
        {
            var call = _apiRepository.ReadUrlAsync($"http://store.steampowered.com/api/appdetails?appids={appId}").ConfigureAwait(false);
            var model = JsonConvert.DeserializeObject<GetAppDetailsResponse>(await call);

            if (!model.AppDetails.Success)
            {
                return null;
            }

            var game = model.AppDetails.Game;

            return new Game
            {
                AppId = game.AppId,
                Name = game.Name,
                StoreLogoUrl = game.LogoUrl
            };
        }
    }
}
