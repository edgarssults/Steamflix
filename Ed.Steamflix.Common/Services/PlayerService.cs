using Newtonsoft.Json;
using Ed.Steamflix.Common.Models;
using System.Threading.Tasks;
using Ed.Steamflix.Common.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace Ed.Steamflix.Common.Services
{
    public class PlayerService
    {
        private readonly string _serviceName = "IPlayerService";
        private readonly IApiRepository _apiRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="apiRepository">API repository implementation.</param>
        public PlayerService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
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
            var call = _apiRepository.ApiCallAsync(
                _serviceName,
                "GetOwnedGames",
                "v1",
                $"steamid={steamId}&include_appinfo=1"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<GetOwnedGamesResponse>(await call);

            // Re-order the game list
            model.OwnedGames.Games = model.OwnedGames.Games.OrderByDescending(g => g.PlaytimeForever).ToList();

            return model.OwnedGames.Games;
        }

        /// <summary>
        /// Retrieves info for one game.
        /// </summary>
        /// <remarks>
        /// https://wiki.teamfortress.com/wiki/WebAPI/GetOwnedGames
        /// </remarks>
        /// <param name="steamId">Steam user identifier.</param>
        /// <param name="appId">Steam app identifier.</param>
        /// <returns></returns>
        public async Task<Game> GetGameInfoAsync(string steamId, int appId)
        {
            var call = _apiRepository.ApiCallAsync(
                _serviceName,
                "GetOwnedGames",
                "v1",
                $"steamid={steamId}&include_appinfo=1&appids_filter[0]={appId}"
            ).ConfigureAwait(false);

            var model = JsonConvert.DeserializeObject<GetOwnedGamesResponse>(await call);

            return model.OwnedGames.Games.FirstOrDefault();
        }
    }
}
