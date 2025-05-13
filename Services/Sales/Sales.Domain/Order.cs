using MainTest.Framework.Entity;

namespace Sales.Domain;

public class Order : BaseEntity
{
    public OrderStatus OrderStatus { get; set; }
    public string CustomerName { get; set; }
    public double TotalAmount { get; set; }
}
