using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityTool.ViewModels;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<DepartamentAddViewModel>()
            .AddTransient<GroupAddViewModel>()
            ;
    }
}
