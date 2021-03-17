using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Core.Abstractions;
using UrlShortener.Core.Models;
using UrlShortener.WebApi.Models.Requests;
using UrlShortener.WebApi.Models.Responses;

namespace UrlShortener.WebApi.Controllers
{
    [ApiController]
    public class ShortenController : Controller
    {
        private readonly IShortenService _shortenService;
        private readonly IRepository<ShortenedUrl> _urlRepository;
        private readonly IMapper _mapper;

        public ShortenController(IShortenService shortenService, IRepository<ShortenedUrl> urlRepository, IMapper mapper)
        {
            _shortenService = shortenService;
            _urlRepository = urlRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/shorten")]
        public async Task<IActionResult> ShortenUrl(ShortenUrlRequest request)
        {
            var shortUrl = await _shortenService.ShortenUrl(request.Url);
            return Ok($"{Request.Scheme}://{Request.Host}/{shortUrl}");
        }

        [HttpGet]
        [Route("{url}")]
        public async Task<IActionResult> RedirectToUrl(string url)
        {
            var shortenedUrl = await _shortenService.IncreaseCounter(url);
            if (shortenedUrl == null)
            {
                return NotFound();
            }

            return Redirect(shortenedUrl.LongUrl);
        }

        [HttpGet]
        [Route("/shortened")]
        public async Task<IActionResult> GetShortenedUrls()
        {
            var shortenedUrls = await _urlRepository.GetAllAsync();
            return Ok(shortenedUrls.Select(_mapper.Map<ShortenedUrlResponse>));
        }
    }
}
