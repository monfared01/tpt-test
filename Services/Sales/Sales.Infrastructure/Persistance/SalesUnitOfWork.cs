using MainTest.Framework.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Common.Persistence.Interfaces;
using Sales.Infrastructure.Persistance.Repository;

namespace Sales.Infrastructure.Persistance
{
    public class SalesUnitOfWork : UnitOfWork, ISalesUnitOfWork
    {
        public SalesUnitOfWork(SalesDbContext dbContext) : base(dbContext)
        {
        }

        public SalesUnitOfWork(SalesDbContext dbContext, Guid userId) : base(dbContext,userId)
        {
            dbContext.UserId = userId;
        }

        IOrderRepository orderRepository;
        public IOrderRepository OrderRepository => orderRepository ??= new OrderRepository(dbContext);

    }
    public static class UnitOfWorkExtensions
    {
        public static void AddSalesUnitOfWork(this IServiceCollection services, Action<UnitOfWorkOptions> configureOptions)
        {
            services.AddScoped<ISalesUnitOfWork>(serviceProvider =>
            {
                var dbContext = serviceProvider.GetRequiredService<SalesDbContext>();

                var options = new UnitOfWorkOptions();
                configureOptions(options);

                return new SalesUnitOfWork(dbContext, options.UserId);
            });
        }
    }
}
