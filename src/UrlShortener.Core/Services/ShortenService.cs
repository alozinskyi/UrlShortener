using System;
using System.Threading.Tasks;
using UrlShortener.Core.Abstractions;
using UrlShortener.Core.Models;

namespace UrlShortener.Core.Services
{
    public class ShortenService : IShortenService
    {
        private readonly IRepository<ShortenedUrl> _urlRepository;

        public ShortenService(IRepository<ShortenedUrl> urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<string> ShortenUrl(string longUrl)
        {
            var existingUrls = await _urlRepository.SearchFirstAsync(nameof(ShortenedUrl.LongUrl), longUrl.ToLower());
            if (existingUrls != null)
            {
                throw new InvalidOperationException($"Url {longUrl} already exists");
            }

            var newId = Guid.NewGuid();
            var shortenedUrl = new ShortenedUrl(newId, longUrl, GetShortUrlFromId(newId));
            await _urlRepository.AddAsync(shortenedUrl);
            return shortenedUrl.ShortUrl;
        }

        public async Task<ShortenedUrl> IncreaseCounter(string shortUrl)
        {
            var existingUrl = await _urlRepository.SearchFirstAsync(nameof(ShortenedUrl.ShortUrl), shortUrl.ToLower());
            if (existingUrl == null)
            {
                return null;
            }

            existingUrl.Counter++;
            await _urlRepository.UpdateAsync(existingUrl.Id, existingUrl);
            return existingUrl;
        }

        private static string GetShortUrlFromId(Guid id)
        {
            return id.ToString("N").Substring(0, Constants.KeyLength);
        }
    }
}
