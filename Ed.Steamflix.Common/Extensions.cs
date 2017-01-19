using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Ed.Steamflix.Common
{
    public static class Extensions
    {
        /// <summary>
        /// Extracts a CookieContainer out of a HttpResponseMessage.
        /// </summary>
        /// <remarks>
        /// http://stackoverflow.com/questions/14681144/httpclient-not-storing-cookies-in-cookiecontainer
        /// </remarks>
        /// <returns>A cookie container.</returns>
        public static CookieContainer ReadCookies(this HttpResponseMessage response)
        {
            var pageUri = response.RequestMessage.RequestUri;

            var cookieContainer = new CookieContainer();
            IEnumerable<string> cookies;
            if (response.Headers.TryGetValues("set-cookie", out cookies))
            {
                foreach (var c in cookies)
                {
                    cookieContainer.SetCookies(pageUri, c);
                }
            }

            return cookieContainer;
        }
    }
}