using Ed.Steamflix.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Ed.Steamflix.Common.Converters
{
    /// <summary>
    /// Custom JSON converter for reading app details responses.
    /// </summary>
    public class AppDetailsResponseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GetAppDetailsResponse);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Not expecting this to write
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var response = existingValue as GetAppDetailsResponse ?? new GetAppDetailsResponse();
            var jsonObject = JObject.Load(reader);

            // Extecting the first element of the first object to be the app details
            response.AppDetails = jsonObject.First.First.ToObject<AppDetails>();

            return response;
        }
    }
}