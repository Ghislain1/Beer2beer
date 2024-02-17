using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Beer2beer.API.ActionResults;

// Aufgabe 26: ActionResult concept
public sealed class InternalServerErrorObjectResult : IActionResult
{
    private readonly Exception exception;
    public InternalServerErrorObjectResult(Exception exception)
    {
        this.exception = exception;
    }


    public async Task ExecuteResultAsync(ActionContext actionContext)
    {
        await Task.Delay(500);
        var result = new
        {
            exception.Message,
            exception.StackTrace,
            exception.Source,
            exception.GetType().Name,
        };
        var objectResult = new ObjectResult(result);
        actionContext.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await objectResult.ExecuteResultAsync(actionContext);
    }
}
