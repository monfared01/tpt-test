using AutoMapper;
using FluentResults;
using MainTest.Framework.Extensions;
using MediatR;
using Sales.Application.Common.Persistence.Interfaces;
using Sales.Application.Query.Order;
using Sales.Application.Response;

namespace Sales.Application.QueryHandler.Order
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, FluentResults.Result<OrderResponse>>
    {
        private readonly ISalesUnitOfWork salesUnitOfWork;
        private readonly IMapper mapper;

        public GetOrderByIdQueryHandler(ISalesUnitOfWork salesUnitOfWork, IMapper mapper)
        {
            this.salesUnitOfWork = salesUnitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<OrderResponse>();
            try
            {
                var entity = await salesUnitOfWork.OrderRepository.GetOneAsync(p => p.Id == request.Id);
                var OrderResponse = mapper.Map<OrderResponse>(entity);

                result.WithValue(OrderResponse);
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