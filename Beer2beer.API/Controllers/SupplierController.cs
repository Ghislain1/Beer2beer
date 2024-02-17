using Beer2beer.API.ActionResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beer2beer.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


    //   GET: api/supplier
    [HttpGet]
    public IActionResult Get()
    {

        try
        {
            var customer = Summaries[10];
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return new InternalServerErrorObjectResult(ex);
            // return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }
}
