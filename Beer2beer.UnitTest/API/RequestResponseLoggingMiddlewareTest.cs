namespace Beer2beer.UnitTest.API;

using Beer2beer.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

public class RequestResponseLoggingMiddlewareTest
{
    private Mock<ILogger<RequestResponseLoggingMiddleware>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<RequestResponseLoggingMiddleware>>();
    }

    [Test]
    public async Task Invoke_LogsRequestAndResponse_WhenLogLevelInformationIsEnabled()
    {
        // Arrange
        _loggerMock.Setup(l => l.IsEnabled(LogLevel.Information)).Returns(true);

        bool nextCalled = false;
        RequestDelegate next = (HttpContext ctx) =>
        {
            nextCalled = true;
            ctx.Response.StatusCode = 200;
            return Task.CompletedTask;
        };

        var middleware = new RequestResponseLoggingMiddleware(next, _loggerMock.Object);
        var context = new DefaultHttpContext();
        context.Request.Method = "GET";
        context.Request.Path = "/api/test";

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.That(nextCalled, Is.True);
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v != null && v.ToString()!.Contains("Request received")),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);

        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v != null && v.ToString()!.Contains("Response sent")),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Test]
    public async Task Invoke_SkipsLoggingDetails_WhenLogLevelInformationIsDisabled()
    {
        // Arrange
        _loggerMock.Setup(l => l.IsEnabled(LogLevel.Information)).Returns(false);

        bool nextCalled = false;
        RequestDelegate next = (HttpContext ctx) =>
        {
            nextCalled = true;
            ctx.Response.StatusCode = 200;
            return Task.CompletedTask;
        };

        var middleware = new RequestResponseLoggingMiddleware(next, _loggerMock.Object);
        var context = new DefaultHttpContext();
        context.Request.Method = "GET";
        context.Request.Path = "/api/test";

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.That(nextCalled, Is.True);
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Never);
    }
}
