using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Services;
using UniversityTool.Domain.Services;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.DataServices.Impementations;
using UniversityTool.Services.WindowsServices.Implementaions;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddSingleton<IMessageBusService, MessageBusService>()
            .AddSingleton<IMainWindowService, MainWindowService>()
            .AddSingleton<IDepartamentAddWindowService, DepartamentAddWindowService>()
            .AddSingleton<IGroupAddWindowService, GroupAddWindowService>()
            ;
    }
}
