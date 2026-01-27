using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Persistence;
using EcommerceDev.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceDev.Infrastructure;

public static class InfrastructureModule
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure()
        {
            services
                .AddData()
                .AddRepositories();

            return services;
        }

        private IServiceCollection AddData()
        {
            services
                .AddDbContext<EcommerceDbContext>(options => options.UseInMemoryDatabase("EcommerceDb"));

            return services;
        }

        private IServiceCollection AddRepositories()
        {
            services
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IProductCategoryRepository, ProductCategoryRepository>()
                .AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
