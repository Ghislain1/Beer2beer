namespace Beer2beer.UnitTest.API;

using Beer2beer.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public class RequestResponseLoggingMiddlewareTest
{
    private Mock<ILogger<RequestResponseLoggingMiddleware>> _loggerMock;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<RequestResponseLoggingMiddleware>>();
    }

    [Test]
    public async Task Invoke_WhenLoggingDisabled_DoesNotLogOrEvaluateHeaders()
    {
        // Arrange
        _loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(false);

        var context = new DefaultHttpContext();
        context.Request.Method = "GET";
        context.Request.Path = "/api/customer";

        bool nextCalled = false;
        RequestDelegate next = (ctx) =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        };

        var middleware = new RequestResponseLoggingMiddleware(next, _loggerMock.Object);

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.That(nextCalled, Is.True);
        _loggerMock.Verify(x => x.IsEnabled(LogLevel.Information), Times.AtLeastOnce);
        _loggerMock.Verify(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<System.Exception>(),
            It.IsAny<System.Func<It.IsAnyType, System.Exception?, string>>()), Times.Never);
    }

    [Test]
    public async Task Invoke_WhenLoggingEnabled_LogsRequestAndResponse()
    {
        // Arrange
        _loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true);

        var context = new DefaultHttpContext();
        context.Request.Method = "GET";
        context.Request.Path = "/api/customer";
        context.Response.StatusCode = 200;

        bool nextCalled = false;
        RequestDelegate next = (ctx) =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        };

        var middleware = new RequestResponseLoggingMiddleware(next, _loggerMock.Object);

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.That(nextCalled, Is.True);
        _loggerMock.Verify(x => x.IsEnabled(LogLevel.Information), Times.AtLeastOnce);
        _loggerMock.Verify(x => x.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<System.Exception>(),
            It.IsAny<System.Func<It.IsAnyType, System.Exception?, string>>()), Times.Exactly(4));
    }
}
