namespace UrlShortener.WebApi.Models.Responses
{
    public class ExceptionResponse
    {
        public ExceptionResponse(string message, string details)
        {
            Message = message;
            Details = details;
        }

        public string Message { get; }

        public string Details { get; }
    }
}
