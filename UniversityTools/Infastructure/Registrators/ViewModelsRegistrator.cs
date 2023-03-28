using Microsoft.Extensions.DependencyInjection;
using UniversityTool.ViewModels;
using UniversityTool.ViewModels.AddingViemModels;
using UniversityTool.ViewModels.ControlsViewModels;
using UniversityTool.ViewModels.UpdatingViewModels;

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
            .AddTransient<DepartamentUpdateViewModel>()
            ;
    }
}
