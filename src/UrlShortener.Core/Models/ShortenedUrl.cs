using System;

namespace UrlShortener.Core.Models
{
    public class ShortenedUrl : BaseEntity
    {
        public ShortenedUrl(string longUrl, string shortUrl)
        {
            LongUrl = longUrl.ToLower();
            ShortUrl = shortUrl;
            CreatedOn = DateTime.UtcNow;
        }

        public string LongUrl { get; protected set; }

        public string ShortUrl { get; protected set; }

        public int Counter { get; set; }

        public DateTime CreatedOn { get; protected set; }
    }
}
