using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Services
{
    /// <summary>
    /// Service class for Steam's broadcast page.
    /// </summary>
    public class BroadcastService
    {
        private readonly Regex _broadcastsRegex = new Regex(@"<div[^>]*class=""[^""]*Broadcast_Card[^>]*>\s*<a[^>]*href=""(?<Url>[^""]*)"".*?<div\s*style=""clear:\s*left""></div>\s*</div>", RegexOptions.Singleline);
        private readonly Regex _userNameRegex = new Regex(@"apphub_CardContentAuthorName[^>]*>\s*<a[^>]*>(?<Name>[^<]*)", RegexOptions.Singleline);
        private readonly Regex _imageRegex = new Regex(@"class=""[^""]*apphub_CardContentPreviewImage[^""]*""[^>]*src=""(?<Url>[^""]+)""", RegexOptions.Singleline);

        private readonly ICommunityRepository _communityRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="communityRepository">Community repository implementation.</param>
        public BroadcastService(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        /// <summary>
        /// Gets a list of available broadcasts for a game.
        /// </summary>
        /// <param name="appId">Application identifier.</param>
        /// <returns>List of broadcasts.</returns>
        public async Task<List<Broadcast>> GetBroadcastsAsync(int appId)
        {
            var broadcasts = new List<Broadcast>();
            var html = await _communityRepository.GetBroadcastHtmlAsync(appId).ConfigureAwait(false);

            foreach (Match match in _broadcastsRegex.Matches(html))
            {
                broadcasts.Add(new Broadcast
                {
                    WatchUrl = match.Groups["Url"].Value.Trim(),
                    UserName = _userNameRegex.Match(match.Value).Groups["Name"].Value,
                    ImageUrl = _imageRegex.IsMatch(match.Value) ? _imageRegex.Match(match.Value).Groups["Url"].Value : null
                });
            }

            return broadcasts;
        }

        // TODO: Method for getting more broadcasts for the same app
    }
}
