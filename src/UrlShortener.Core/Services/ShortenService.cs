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
            var existingLongUrl = await _urlRepository.SearchFirstAsync(nameof(ShortenedUrl.LongUrl), longUrl.ToLower());
            if (existingLongUrl != null)
            {
                throw new InvalidOperationException($"Url {longUrl} already exists");
            }

            while (true)
            {
                var shortUrl = Guid.NewGuid().ToString("N").Substring(0, Constants.KeyLength);
                var existingShortUrl = await _urlRepository.SearchFirstAsync(nameof(ShortenedUrl.ShortUrl), shortUrl);
                if (existingShortUrl != null)
                {
                    continue;
                }

                var shortenedUrl = new ShortenedUrl(longUrl, shortUrl);
                await _urlRepository.AddAsync(shortenedUrl);
                return shortUrl;
            }
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
    }
}
