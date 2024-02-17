using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer2beer.Core.Entities;
public class CustomerViewModel
{
    public int Id { get; set; }
    [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;
    [Required, DataType(DataType.EmailAddress), StringLength(maximumLength: 100, MinimumLength = 5)]
    public string Email { get; set; } = string.Empty;
    public decimal? Balance { get; set; } = 0;
}
