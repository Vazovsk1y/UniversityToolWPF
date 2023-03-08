using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Factory;
using UniversityTool.DataBase.Services.Registrator;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class DataRegistrator
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services) => services
            .AddScoped<UniversityToolDbContextFactory>()
            .AddDbRepositories()
            ;
    }
}
