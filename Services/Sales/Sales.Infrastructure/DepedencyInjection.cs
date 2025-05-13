using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sales.Application.Common.Interface;
using Sales.Infrastructure.Persistance;

namespace Sales.Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings.Main>(
                configuration.GetSection(AppSettings.Main.Section));

            services.AddDbContext<SalesDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("SalesConnectionStrings")));

            services.AddSalesUnitOfWork(options =>
            {
                var currentUserAccessor = services.BuildServiceProvider().GetRequiredService<ICurrentUserAccessor>();
                options.UserId = currentUserAccessor.UserId;
            });

            return services;
        }
    }
}
