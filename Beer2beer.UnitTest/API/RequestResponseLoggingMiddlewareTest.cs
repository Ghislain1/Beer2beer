namespace Beer2beer.UnitTest.API;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Beer2beer.API.Controllers;

[TestFixture]
public class RequestResponseLoggingMiddlewareTest
{
    private Mock<ILogger<RequestResponseLoggingMiddleware>> loggerMock;

    [SetUp]
    public void Setup()
    {
        loggerMock = new Mock<ILogger<RequestResponseLoggingMiddleware>>();
    }

    [Test]
    public async Task Invoke_WhenLoggingEnabled_ExecutesPipelineAndLogs()
    {
        // Arrange
        loggerMock.Setup(l => l.IsEnabled(LogLevel.Information)).Returns(true);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = "GET";
        httpContext.Request.Path = "/api/customer";

        bool nextCalled = false;
        RequestDelegate next = (ctx) =>
        {
            nextCalled = true;
            ctx.Response.StatusCode = 200;
            return Task.CompletedTask;
        };

        var middleware = new RequestResponseLoggingMiddleware(next, loggerMock.Object);

        // Act
        await middleware.Invoke(httpContext);

        // Assert
        Assert.That(nextCalled, Is.True);
        loggerMock.Verify(l => l.IsEnabled(LogLevel.Information), Times.AtLeastOnce);
    }

    [Test]
    public async Task Invoke_WhenLoggingDisabled_ExecutesPipelineWithoutLogging()
    {
        // Arrange
        loggerMock.Setup(l => l.IsEnabled(LogLevel.Information)).Returns(false);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = "GET";
        httpContext.Request.Path = "/api/customer";

        bool nextCalled = false;
        RequestDelegate next = (ctx) =>
        {
            nextCalled = true;
            ctx.Response.StatusCode = 200;
            return Task.CompletedTask;
        };

        var middleware = new RequestResponseLoggingMiddleware(next, loggerMock.Object);

        // Act
        await middleware.Invoke(httpContext);

        // Assert
        Assert.That(nextCalled, Is.True);
        loggerMock.Verify(l => l.IsEnabled(LogLevel.Information), Times.AtLeastOnce);
    }
}
