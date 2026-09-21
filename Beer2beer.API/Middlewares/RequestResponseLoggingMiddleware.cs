using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Beer2beer.API.Controllers;
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Optimization: Check if Information level logging is enabled before evaluating log messages
        // or building string representations of headers on every HTTP request.
        if (_logger.IsEnabled(LogLevel.Information))
        {
            LogRequest(context.Request);
        }

        // Call the next middleware in the pipeline
        await _next(context);

        if (_logger.IsEnabled(LogLevel.Information))
        {
            LogResponse(context.Response);
        }
    }

    private void LogRequest(HttpRequest request)
    {
        _logger.LogInformation("Request received: {Method} {Path}", request.Method, request.Path);
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Request headers: {Headers}", GetHeadersAsString(request.Headers));
        }
    }

    private void LogResponse(HttpResponse response)
    {
        _logger.LogInformation("Response sent: {StatusCode}", response.StatusCode);
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Response headers: {Headers}", GetHeadersAsString(response.Headers));
        }
    }

    private string GetHeadersAsString(IHeaderDictionary headers)
    {
        var stringBuilder = new StringBuilder();
        foreach (var (key, value) in headers)
        {
            stringBuilder.AppendLine($"{key}: {value}");
        }
        return stringBuilder.ToString();
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class RequestResponseLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }

}
