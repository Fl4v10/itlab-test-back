using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace testeItLab.web.Utils
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddModelMapping(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                AddProfiles(mc);
            });
            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
            return services;
        }

        private static void AddProfiles(IMapperConfigurationExpression mc)
        {
            var profiles = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            mc.AddMaps(profiles);
        }
    }
}
