using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ed.Steamflix.Api.Controllers
{
    [Route("api/[controller]")]
    public class TileController : Controller
    {
        private readonly GameService _gameService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gameService"></param>
        public TileController(GameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Tile without Steam ID.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpGet("{index}")]
        public string Get(int index)
        {
            var content = GetTileContentAsync(index).Result;
            return content.GetContent();
        }

        /// <summary>
        /// Tile with Steam ID.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="steamId"></param>
        /// <returns></returns>
        [HttpGet("{index}/{steamId}")]
        public string Get(int index, string steamId)
        {
            var content = GetTileContentAsync(index, steamId).Result;
            return content.GetContent();
        }

        /// <summary>
        /// Gets tile content by index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="steamId"></param>
        /// <returns></returns>
        private async Task<TileContent> GetTileContentAsync(int index, string steamId = null)
        {
            var game = await GetSingleGameAsync(index, steamId);

            // No text on wide tile because game images contain the game name
            // No medium live tile because game images contain text, which would usually be cropped
            // No large tile because game images aren't as big
            // Perhaps we could show some community content on large tile?

            var content = new TileContent
            {
                Visual = new TileVisual
                {
                    TileWide = new TileBinding
                    {
                        Content = new TileBindingContentAdaptive
                        {
                            BackgroundImage = new TileBackgroundImage()
                            {
                                Source = game.FormattedLogoUrl
                            }
                        }
                    }
                }
            };

            return content;
        }

        /// <summary>
        /// Gets a game from either recently played or popular games by index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="steamId"></param>
        /// <returns></returns>
        private async Task<Game> GetSingleGameAsync(int index, string steamId)
        {
            Game selectedGame = null;
            int maxRecentGames = 0;
            var recentGames = new List<Game>();

            // Get a recently played game
            if (!string.IsNullOrEmpty(steamId))
            {
                recentGames = await _gameService.GetRecentlyPlayedGamesAsync(steamId);
                if (recentGames != null && recentGames.Count > index)
                {
                    selectedGame = recentGames[index];
                }
                else
                {
                    // Save recent game count for later
                    maxRecentGames = recentGames.Count;
                }
            }

            // If not enough recently played games, get a popular game instead
            if (selectedGame == null)
            {
                // Reset the index to start from the beginning of the popular game list
                if (maxRecentGames > 0)
                {
                    index -= maxRecentGames;
                }

                var popularGames = await _gameService.GetPopularGamesAsync();
                if (popularGames != null && popularGames.Count > index)
                {
                    selectedGame = popularGames[index];
                }
            }

            return selectedGame;
        }
    }
}
