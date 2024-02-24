using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Beer2beer.API.ActionFilters;
// 31. Action Filter Concept: why? add a logic to control 

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)] // Inform compiler where you can use it
public class ProfilerActionFilterAttribute : Attribute, IActionFilter
{
    private readonly Stopwatch stopwatch = new Stopwatch();
    public void OnActionExecuted(ActionExecutedContext context)
    {
        this.stopwatch.Stop();
        Debug.WriteLine($" ===========================================\n Action spends Time: {this.stopwatch.ElapsedMilliseconds} ms \n");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        this.stopwatch.Start();
        Debug.WriteLine($"\n Starting Action: {context.ActionDescriptor.DisplayName}");
    }
}
