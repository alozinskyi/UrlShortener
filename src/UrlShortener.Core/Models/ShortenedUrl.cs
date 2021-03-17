using System;

namespace UrlShortener.Core.Models
{
    public class ShortenedUrl : BaseEntity
    {
        public ShortenedUrl(Guid id, string longUrl, string shortUrl)
        {
            Id = id;
            LongUrl = longUrl.ToLower();
            CreatedOn = DateTime.UtcNow;
            ShortUrl = shortUrl;
        }

        public string LongUrl { get; protected set; }

        public string ShortUrl { get; protected set; }

        public int Counter { get; set; }

        public DateTime CreatedOn { get; protected set; }
    }
}
