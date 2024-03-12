using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class RequestLoggingMiddleware : IMiddleware
{
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    //public async Task InvokeAsync(HttpContext context, Func<Task> next)
    //{
    //    // Log information about the incoming request
    //    _logger.LogInformation($"Request received: {context.Request.Method} {context.Request.Path}");

    //    // Call the next middleware in the pipeline
    //    await next();
    //}

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Log information about the incoming request
          _logger.LogInformation($"Request received: {context.Request.Method} {context.Request.Path}");

        // Call the next middleware in the pipeline
        //await next();
    }
}
