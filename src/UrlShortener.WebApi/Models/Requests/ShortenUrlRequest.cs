using FluentValidation;
using System;

namespace UrlShortener.WebApi.Models.Requests
{
    public class ShortenUrlRequest
    {
        public string Url { get; set; }
    }

    public class ShortenUrlRequestValidator : AbstractValidator<ShortenUrlRequest>
    {
        public ShortenUrlRequestValidator()
        {
            RuleFor(x => x.Url)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Url must be valid");
        }
    }
}
