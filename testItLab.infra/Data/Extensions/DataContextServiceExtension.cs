using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using testItLab.infra.Data.Context;

namespace testItLab.infra.Data.Extensions
{
    public static class DataContextServiceExtension
    {
        public static IServiceCollection AddDataContext(
               this IServiceCollection services
               )
        {
            //TODO: NO FUTURO, AO USAR UMA BASE REAL BUSCAR DE UMA VARIÁVEL DE AMBIENTE
            services.AddDbContext<TestItLabDbContext>(options =>
                 options.UseInMemoryDatabase("itLabTesteDb")
                    );
            return services;
        }
    }
}
