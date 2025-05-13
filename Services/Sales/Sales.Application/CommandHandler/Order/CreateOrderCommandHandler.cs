using AutoMapper;
using FluentResults;
using MainTest.Framework.Extensions;
using MediatR;
using Sales.Application.Command.Order;
using Sales.Application.Common.Persistence.Interfaces;

namespace Sales.Application.CommandHandler.Order
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, FluentResults.Result<Guid>>
    {
        private readonly ISalesUnitOfWork salesUnitOfWork;
        private readonly IMapper mapper;

        public CreateOrderCommandHandler(ISalesUnitOfWork salesUnitOfWork, IMapper mapper)
        {
            this.salesUnitOfWork = salesUnitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<Guid>();
            try
            {
                var entity = mapper.Map<Domain.Order>(request);
                salesUnitOfWork.OrderRepository.Create(entity);

                await salesUnitOfWork.SaveAsync();
                result.WithValue(entity.Id);
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