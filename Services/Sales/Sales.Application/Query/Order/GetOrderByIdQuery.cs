using MediatR;
using Sales.Application.Response;

namespace Sales.Application.Query.Order
{
    public class GetOrderByIdQuery : IRequest<FluentResults.Result<OrderResponse>>
    {
        public Guid Id { get; set; }
    }
}