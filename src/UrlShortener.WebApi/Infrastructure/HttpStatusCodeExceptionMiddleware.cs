using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UrlShortener.Core;
using UrlShortener.WebApi.Models.Responses;

namespace UrlShortener.WebApi.Infrastructure
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly ILogger<HttpStatusCodeExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILogger<HttpStatusCodeExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error with method {context.Request.Method} {context.Request.Path}");
                context.Response.Clear();
                context.Response.ContentType = Constants.DefaultMimeType;
                var response = new ExceptionResponse(ex.Message, hostingEnvironment.IsDevelopment() ? ex.StackTrace : null);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response), context.RequestAborted);
            }
        }
    }
}