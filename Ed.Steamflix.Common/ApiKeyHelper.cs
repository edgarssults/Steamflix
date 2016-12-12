using System;
using System.IO;

namespace Ed.Steamflix.Common
{
    public class ApiKeyHelper
    {
        public static string ApiKey { get; } = GetApiKey();

        private static string GetApiKey()
        {
            try
            {
                using (var stream = File.OpenRead(@"Ed.Steamflix.Common\apikey.txt"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find and API key. Please provide one in apikey.txt", ex);
            }
        }
    }
}
