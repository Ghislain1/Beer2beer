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
        // Log the incoming request
        LogRequest(context.Request);

        // Call the next middleware in the pipeline
        await _next(context);

        // Log the outgoing response
        LogResponse(context.Response);
    }

    private void LogRequest(HttpRequest request)
    {
        // Optimization: Use structured logging message template to prevent string interpolation allocation per request
        _logger.LogInformation("Request received: {Method} {Path}", request.Method, request.Path);
        _logger.LogInformation("Request headers: {Headers}", GetHeadersAsString(request.Headers));
    }

    private void LogResponse(HttpResponse response)
    {
        // Optimization: Use structured logging message template to prevent string interpolation allocation per request
        _logger.LogInformation("Response sent: {StatusCode}", response.StatusCode);
        _logger.LogInformation("Response headers: {Headers}", GetHeadersAsString(response.Headers));
    }

    private static string GetHeadersAsString(IHeaderDictionary headers)
    {
        if (headers.Count == 0)
        {
            return string.Empty;
        }

        // Optimization: Efficiently join header key-value pairs without StringBuilder re-allocations
        return string.Join(Environment.NewLine, headers.Select(h => $"{h.Key}: {h.Value}"));
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
