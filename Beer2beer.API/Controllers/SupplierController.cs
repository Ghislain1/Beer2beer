using Beer2beer.API.ActionResults;
using Beer2beer.Core.Entities;
using Beer2beer.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beer2beer.API.Controllers;


[ApiController] //  implicts provide [FromBody ]for each methods
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly ILogger<SupplierController> logger;
    private readonly ISupplierService supplierService;

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
    [HttpPost]
    public async Task<ActionResult> CreateBatch(int? supplierId, [FromBody] IEnumerable<ArticleViewModel> articles)
    {
        if (supplierId == null)
        {
            return NotFound();
        }
        var supplier = await this.supplierService.GetSupplierAsync(supplierId.Value);
        foreach (var article in articles)
        {

            supplier.Suppliers.Add(article);
        }
        return Ok(supplier);
    }
}
