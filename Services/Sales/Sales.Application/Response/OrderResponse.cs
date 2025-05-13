using MainTest.Framework.Extensions;
using MainTest.Framework.Mapping.Interfaces;
using Sales.Domain;

namespace Sales.Application.Response;

public class OrderResponse : IOneWayReverseMap<Domain.Order>
{
    public Guid Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string OrderStatusDesc { get => OrderStatus.GetDescription(); }
    public string CustomerName { get; set; }
    public double TotalAmount { get; set; }
    public string CreatedOn { get; set; }
}
