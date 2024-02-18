
namespace Beer2beer.Core.Entities;

public class Article : Base<int>
{
    public string ArticleNumber { get; set; } = null!;
    public decimal Price { get; set; }
    public Supplier Supplier { get; set; } = null!;
}