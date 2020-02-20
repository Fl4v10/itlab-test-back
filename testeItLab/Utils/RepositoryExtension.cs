using Microsoft.Extensions.DependencyInjection;
using testeItLab.domain.Interfaces;
using testItLab.infra.Data.Repositories;

namespace testeItLab.web.Utils
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
