using SkyCommerce.Domain.Common;

namespace SkyCommerce.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public Guid OrderId { get; private set; }

    public OrderItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
