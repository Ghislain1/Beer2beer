
//  Understand Identity at ==> https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0
namespace Beer2beer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// Abschnitt 5:
// 48. Models Preparation.
public class AppUser : IdentityUser<int>
{
    public string Name { get; set; } = default!;
}
