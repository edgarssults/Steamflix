using Ed.Steamflix.Common.Converters;
using Newtonsoft.Json;

namespace Ed.Steamflix.Common.Models
{
    [JsonConverter(typeof(AppDetailsResponseConverter))]
    public class GetAppDetailsResponse
    {
        public AppDetails AppDetails { get; set; }
    }
}
