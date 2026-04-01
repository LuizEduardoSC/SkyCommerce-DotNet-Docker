using SkyCommerce.Domain.Common;

namespace SkyCommerce.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public Product(string name, string description, decimal price, int stock, Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
    }

    public void Update(string name, string description, decimal price, int stock, Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ReduceStock(int quantity)
    {
        if (Stock < quantity)
            throw new Exception("Insufficient stock.");
        Stock -= quantity;
    }
}
