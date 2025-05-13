using MainTest.Framework.ApiResponse;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Sales.Application;
using Sales.Application.Common.Interface;
using Sales.Infrastructure;
using System.IO.Compression;

namespace Sales.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<AppSettings.Main>(
                builder.Configuration.GetSection(AppSettings.Main.Section));

            builder.Services.AddControllersWithViews()
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.InvalidModelStateResponseFactory = context => context.ToApiResponse();
               });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserAccessor, Infrastructure.Utility.CurrentUserAccessor>();
            

            builder.Services.AddApplication(builder.Configuration)
                            .AddInfrastructure(builder.Configuration);


            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales.API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales.API v1"));
            app.UseCors(options =>
                options.AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowAnyMethod());

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
