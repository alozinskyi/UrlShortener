using AutoMapper;
using UrlShortener.Core.Models;
using UrlShortener.WebApi.Models.Responses;

namespace UrlShortener.WebApi.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShortenedUrl, ShortenedUrlResponse>();
        }
    }
}
