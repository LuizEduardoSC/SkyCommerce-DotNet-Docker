using SkyCommerce.Domain.Common;

namespace SkyCommerce.Domain.Entities;

public class Order : BaseEntity
{
    public string CustomerId { get; private set; }
    public decimal TotalPrice { get; private set; }
    public OrderStatus Status { get; private set; }
    public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();

    public Order(string customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Created;
    }

    public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        var item = new OrderItem(productId, productName, quantity, unitPrice);
        Items.Add(item);
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        TotalPrice = Items.Sum(x => x.Quantity * x.UnitPrice);
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetStatus(OrderStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum OrderStatus
{
    Created,
    Paid,
    Shipped,
    Canceled
}
