using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Beer2beer.API.ActionFilters;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)] // Inform compiler where you can use it
public class ExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {

        var result = new
        {
            context.Exception.Message,
            context.Exception.GetType().Name,

        };
        var objectResult = new ObjectResult(result);
        context.HttpContext.Response.StatusCode = 400;


        context.Result = objectResult;
    }
}
