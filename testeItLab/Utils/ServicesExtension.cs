using Microsoft.Extensions.DependencyInjection;
using testeItLab.domain.Interfaces.Services;
using testeItLab.domain.Services;

namespace testeItLab.web.Utils
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
