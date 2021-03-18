namespace UrlShortener.WebApi.Models.Responses
{
    public class ShortenedUrlResponse
    {
        protected ShortenedUrlResponse() { }

        public ShortenedUrlResponse(string shortUrl, string longUrl)
        {
            ShortUrl = shortUrl;
            LongUrl = longUrl;
        }

        public ShortenedUrlResponse(string shortUrl, string longUrl, int counter) : this(shortUrl, longUrl)
        {
            Counter = counter;
        }

        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }

        public int Counter { get; protected set; }
    }
}
