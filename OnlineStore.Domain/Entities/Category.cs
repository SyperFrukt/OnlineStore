namespace OnlineStore.Domain.Entities;

/// <summary>Категория товаров</summary>
public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
