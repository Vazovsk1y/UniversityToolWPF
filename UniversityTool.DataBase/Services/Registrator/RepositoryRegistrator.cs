using Microsoft.Extensions.DependencyInjection;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.DataBase.Services.Registrator
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddDbRepositories(this IServiceCollection services) => services
            .AddTransient(typeof(IDataRepositoryService<>), typeof(DataRepositoryService<>))
            ;
    }
}
