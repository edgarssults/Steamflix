using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Repositories;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ed.Steamflix.Common.Services
{
    /// <summary>
    /// Steam's broadcast page interaction logic.
    /// </summary>
    public class BroadcastService
    {
        private readonly Regex _gameNameRegex = new Regex(@"class=""apphub_AppName[^""]*[^>]*>(?<Name>[^<]*)", RegexOptions.Singleline);
        private readonly Regex _broadcastsRegex = new Regex(@"<div[^>]*class=""[^""]*Broadcast_Card[^>]*>\s*<a[^>]*href=""(?<Url>[^""]*)"".*?<div\s*style=""clear:\s*left""></div>\s*</div>", RegexOptions.Singleline);
        private readonly Regex _userNameRegex = new Regex(@"apphub_CardContentAuthorName[^>]*>\s*<a[^>]*>(?<Name>[^<]*)", RegexOptions.Singleline);
        private readonly Regex _imageRegex = new Regex(@"class=""[^""]*apphub_CardContentPreviewImage[^""]*""[^>]*src=""(?<Url>[^""]+)""", RegexOptions.Singleline);
        private readonly Regex _viewerRegex = new Regex(@"class=""[^""]*apphub_CardContentViewers[^""]*""[^>]*>\s*(?<Viewers>\d+)\s*viewer", RegexOptions.Singleline);

        private readonly ICommunityRepository _communityRepository;

        /// <summary>
        /// Steam's broadcast page interaction logic.
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
        public async Task<GetBroadcastsResponse> GetBroadcasts(int appId)
        {
            var html = await _communityRepository.GetBroadcastHtml(appId).ConfigureAwait(false);

            var broadcasts = new List<Broadcast>();
            foreach (Match match in _broadcastsRegex.Matches(html))
            {
                broadcasts.Add(new Broadcast
                {
                    WatchUrl = match.Groups["Url"].Value.Trim(),
                    UserName = _userNameRegex.Match(match.Value).Groups["Name"].Value,
                    ImageUrl = _imageRegex.IsMatch(match.Value) ? _imageRegex.Match(match.Value).Groups["Url"].Value : null,
                    ViewerCount = _viewerRegex.IsMatch(match.Value) ? int.Parse(_viewerRegex.Match(match.Value).Groups["Viewers"].Value) : (int?)null
                });
            }

            var name = _gameNameRegex.Match(html).Groups["Name"].Value.Trim();

            return new GetBroadcastsResponse
            {
                GameName = name,
                Broadcasts = broadcasts
            };
        }

        // TODO: Method for getting more broadcasts for the same app, currently it gets only the first page
    }
}
