 
namespace Beer2beer.Core.Entities;

public class Article : Base<int>
{
    public Contact Contact { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
}
