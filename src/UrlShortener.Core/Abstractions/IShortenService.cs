using System.Threading.Tasks;
using UrlShortener.Core.Models;

namespace UrlShortener.Core.Abstractions
{
    public interface IShortenService
    {
        Task<string> ShortenUrl(string longUrl);

        Task<ShortenedUrl> IncreaseCounter(string shortUrl);
    }
}