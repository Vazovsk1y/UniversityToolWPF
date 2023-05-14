using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Repositories;
using UniversityTool.DataBase.Repositories.Base;
using UniversityTool.Domain.Repositories;
using UniversityTool.Domain.Repositories.Base;

namespace UniversityTool.DataBase.Registrators.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddDbRepositories(this IServiceCollection services) => services
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddScoped<IDepartamentTreeRepository, DepartamentTreeRepository>()
            ;
    }
}
