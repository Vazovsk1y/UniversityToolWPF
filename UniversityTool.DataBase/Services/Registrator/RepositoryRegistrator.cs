using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;

namespace UniversityTool.DataBase.Services.Registrator
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddDbRepositories(this IServiceCollection services) => services
            .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddTransient<ITreeRepository, TreeRepository>()
            ;
    }
}
