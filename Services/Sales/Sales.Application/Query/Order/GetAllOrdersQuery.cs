using MainTest.Framework.Common;
using MainTest.Framework.Extensions;
using MediatR;
using Sales.Application.Response;
using Sales.Domain;
using System.Linq.Expressions;

namespace Sales.Application.Query.Order
{
    public class GetAllOrdersQuery : ListRequest,
        IRequest<FluentResults.Result<ListResponse<OrderResponse>>>,
        IHasExpressionFilter<Domain.Order>
    {
        public OrderStatus? OrderStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public Expression<Func<Domain.Order, bool>> GetExpression()
        {
            return p =>
                (!OrderStatus.HasValue || p.OrderStatus == OrderStatus.Value) &&
                (!FromDate.HasValue || p.CreatedOn >= FromDate.Value) &&
                (!ToDate.HasValue || p.CreatedOn <= ToDate.Value);
        }
    }
}