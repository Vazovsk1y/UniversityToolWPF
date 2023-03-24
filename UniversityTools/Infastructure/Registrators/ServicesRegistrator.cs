using Microsoft.Extensions.DependencyInjection;
using UniversityTool.Domain.Services;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.DataServices;
using UniversityTool.Services.WindowsServices;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class ServicesRegistrator
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services) => services
            .AddSingleton<IMessageBusService, MessageBusService>()
            .AddSingleton<IMainWindowService, MainWindowService>()
            .AddSingleton<IDepartamentAddWindowService, DepartamentAddWindowService>()
            .AddSingleton<IGroupAddWindowService, GroupAddWindowService>()
            .AddSingleton<IStudentAddWindowService, StudentAddWindowService>()
            ;
    }
}
