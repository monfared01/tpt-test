using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Sales.Application.Validation.Order;

namespace Sales.Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(Application.Mapping.ApplicationProfile).Assembly);

            services.Configure<AppSettings.Main>(
                configuration.GetSection(AppSettings.Main.Section));

            services.AddValidatorsFromAssemblyContaining<CreateOrderValidator>();
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
