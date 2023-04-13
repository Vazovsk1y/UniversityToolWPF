using Microsoft.Extensions.DependencyInjection;
using UniversityTool.ViewModels;
using UniversityTool.ViewModels.ControlsVMs;
using UniversityTool.ViewModels.DepartamentVMs;
using UniversityTool.ViewModels.GroupVMs;
using UniversityTool.ViewModels.StudentVMs;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<TreeViewViewModel>()
            .AddSingleton<MenuViewModel>()
            .AddSingleton<TabsPanelViewModel>()
            .AddTransient<DepartamentAddViewModel>()
            .AddTransient<GroupAddViewModel>()
            .AddTransient<StudentAddViewModel>()
            .AddTransient<DepartamentUpdateViewModel>()
            .AddTransient<GroupUpdateViewModel>()
            .AddTransient<StudentUpdateViewModel>()
            .AddTransient<DepartamentDeleteViewModel>()
            .AddTransient<GroupDeleteViewModel>()
            .AddTransient<StudentDeleteViewModel>()
            .AddTransient<WorkSpaceViewModel>()
            ;
    }
}
