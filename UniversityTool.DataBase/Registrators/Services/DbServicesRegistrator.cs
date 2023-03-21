using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Response;
using UniversityTool.DataBase.Services;
using UniversityTool.Domain.Response;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.DataBase.Registrators.Services
{
    public static class DbServicesRegistrator
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services) => services
            .AddTransient<IDepartamentService, DepartamentService>()
            .AddTransient<IGroupService, GroupService>()
            .AddTransient(typeof(IResponseFactory<>), typeof(ResponseFactory<>))
            .AddTransient<IDepartamentTreeService, DepartamentTreeService>()
            ;
    }
}
