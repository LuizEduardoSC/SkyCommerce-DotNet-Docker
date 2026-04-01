using SkyCommerce.Domain.Common;

namespace SkyCommerce.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public ICollection<Product> Products { get; private set; } = new List<Product>();

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}
