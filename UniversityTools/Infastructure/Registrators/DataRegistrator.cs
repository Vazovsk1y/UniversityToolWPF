using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Factory;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class DataRegistrator
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services) => services
            .AddScoped<UniversityToolDbContextFactory>()
            ;
    }
}
