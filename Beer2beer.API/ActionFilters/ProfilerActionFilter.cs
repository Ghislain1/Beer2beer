using Microsoft.AspNetCore.Mvc.Filters;

namespace Beer2beer.API.ActionFilters;
// 31. Action Filter Concept: why? add a logic to control 

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)] // Inform compiler where you can use it
public class ProfilerActionFilter : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        throw new NotImplementedException();
    }
}
