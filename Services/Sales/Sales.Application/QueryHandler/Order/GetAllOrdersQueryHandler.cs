using AutoMapper;
using FluentResults;
using MainTest.Framework.Common;
using MainTest.Framework.Extensions;
using MediatR;
using Sales.Application.Common.Persistence.Interfaces;
using Sales.Application.Query.Order;
using Sales.Application.Response;
using System.Linq.Dynamic.Core;

namespace Sales.Application.QueryHandler.Order
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, FluentResults.Result<ListResponse<OrderResponse>>>
    {
        private readonly ISalesUnitOfWork salesUnitOfWork;
        private readonly IMapper mapper;

        public GetAllOrdersQueryHandler(ISalesUnitOfWork salesUnitOfWork, IMapper mapper)
        {
            this.salesUnitOfWork = salesUnitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<ListResponse<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<ListResponse<OrderResponse>>();
            try
            {
                var entities = await salesUnitOfWork.OrderRepository.GetAsync(request.GetExpression(), p => p.OrderBy(request.OrderByString()), request.RowSkip(), request.PageSize);
                var count = await salesUnitOfWork.OrderRepository.GetCountAsync(request.GetExpression());

                var response = new ListResponse<OrderResponse>()
                {
                    TotalRowCount = count,
                    Data = entities.Select(mapper.Map<OrderResponse>).ToList(),
                };
                result.WithValue(response);
                result.WithSuccess(string.Empty);

            }
            catch (Exception ex)
            {
                result.WithError(ex.Message);
                result.WithErrors(ex.GetInnerExceptions().Select(e => e.Message));
            }
            return result;
        }
    }
}