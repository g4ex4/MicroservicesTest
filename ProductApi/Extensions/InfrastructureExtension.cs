using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure;
using System;

namespace ProductApi.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ProductContext>(opt
            => opt.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            return services;
        }
    }
}
