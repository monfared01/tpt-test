using MainTest.Framework.Mapping.Interfaces;
using MediatR;
using Sales.Domain;

namespace Sales.Application.Command.Order
{
    public class CreateOrderCommand : IRequest<FluentResults.Result<Guid>>,
        IOneWayMap<Domain.Order>
    {
        public string CustomerName { get; set; }
        public double TotalAmount { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public OrderStatus OrderStatus => OrderStatus.Pending;
    }
}