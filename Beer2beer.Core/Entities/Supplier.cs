namespace Beer2beer.Core.Entities;

public class Supplier : Base<int>
{
    public Contact Contact { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public ICollection<Article> Articles { get; set; } = null!; // ICollection <T> Vs. IList<T>?
}

