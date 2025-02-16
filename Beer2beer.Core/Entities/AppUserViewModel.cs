

namespace Beer2beer.Core.Entities;
using System.ComponentModel.DataAnnotations;

public class AppUserViewModel
{
    [Required, DataType(DataType.EmailAddress), StringLength(maximumLength: 100, MinimumLength = 5)]
    public string Name { get; set; } = string.Empty;
    public int Id { get; set; }  
}