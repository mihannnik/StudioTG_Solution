using Microsoft.Extensions.DependencyInjection;
using StudioTG.Application.Interfaces;
using StudioTG.Infrastructure.Repositories;
using StudioTG.Infrastructure.Services;

namespace StudioTG.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IFieldRepository, FieldsRepository>();
            services.AddTransient<IFieldService, FieldSerivce>();
            services.AddTransient<IFieldSerializationService, FieldSerializationService>();

            return services;
        }
    }
}
