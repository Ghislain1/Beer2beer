using Beer2beer.Core.Entities;
using Beer2beer.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beer2beer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : Controller
{
    private readonly IAppUserService appUserService;
    private readonly ILogger<AdminController> logger;
    public AdminController(ILogger<AdminController> logger, IAppUserService appUserService)
    {
        this.logger = logger;
        this.appUserService = appUserService;
    }

    // get all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<AppUserViewModel>>> GetAll()
    {
        try
        {
            var result = await this.appUserService.GetAppUsersAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving customers");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        // return new AppUser[] { new AppUser() { Email="Ghislain@Ghisa.com", Name="Ghislain" }, new AppUser() { Email="Locka@Ghisa.com", Name="LookUp" } };
    }

    // Post
    [HttpPost("")]
    public async Task<ActionResult<AppUserViewModel>> Post(AppUserViewModel appUserViewModel)
    {
        var result = await this.appUserService.CreateAsync(appUserViewModel);
        return Ok(result);
    }

    // Put
    [HttpPut("")]
    public async Task<ActionResult<AppUserViewModel>> Put(AppUserViewModel appUserViewModel)
    {
        await this.appUserService.UpdateAsync(appUserViewModel);
        var result = await this.appUserService.GetAsync(appUserViewModel.Id);
        return Ok(result);
    }
}
