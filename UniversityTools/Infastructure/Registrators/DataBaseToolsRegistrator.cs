using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Factory;
using UniversityTool.DataBase.Registrators.Repositories;
using UniversityTool.DataBase.Registrators.Services;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class DataBaseToolsRegistrator
    {
        public static IServiceCollection AddDataBaseTools(this IServiceCollection services) => services
            .AddScoped<UniversityToolDbContextFactory>()
            .AddDbRepositories()
            .AddDbServices()
            ;
    }
}
