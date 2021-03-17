using System;

namespace UrlShortener.WebApi.Models.Responses
{
    public class ShortenedUrlResponse
    {
        protected ShortenedUrlResponse() { }

        public ShortenedUrlResponse(string shortUrl, string longUrl, int counter)
        {
            ShortUrl = shortUrl;
            LongUrl = longUrl;
            Counter = counter;
        }

        public string ShortUrl { get; protected set; }

        public string LongUrl { get; protected set; }

        public int Counter { get; protected set; }
    }
}
