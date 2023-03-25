using Microsoft.Extensions.DependencyInjection;
using UniversityTool.ViewModels;
using UniversityTool.ViewModels.ControlsViewModels;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<TreeViewViewModel>()
            .AddSingleton<MenuViewModel>()
            .AddTransient<DepartamentAddViewModel>()
            .AddTransient<GroupAddViewModel>()
            .AddTransient<StudentAddViewModel>()
            ;
    }
}
