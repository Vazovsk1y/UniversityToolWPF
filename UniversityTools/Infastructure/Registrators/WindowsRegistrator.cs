using Microsoft.Extensions.DependencyInjection;
using UniversityTool.Infastructure.Registrators.Base;
using UniversityTool.ViewModels;
using UniversityTool.ViewModels.DepartamentVMs;
using UniversityTool.ViewModels.GroupVMs;
using UniversityTool.ViewModels.StudentVMs;
using UniversityTool.Views.Windows;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class WindowsRegistrator
    {
        public static IServiceCollection AddWindows(this IServiceCollection services) => services
            .AddSingleton(
                s =>
                {
                    var viewModel = s.GetRequiredService<MainWindowViewModel>();
                    var window = new MainWindow { DataContext = viewModel };

                    return window;
                })
            .AddWindowTransient<DepartamentAddViewModel, DepartamentAddWindow>()
            .AddWindowTransient<DepartamentUpdateViewModel, DepartamentUpdateWindow>()
            .AddWindowTransient<DepartamentDeleteViewModel, DepartamentDeleteWindow>()
            .AddWindowTransient<GroupAddViewModel, GroupAddWindow>()
            .AddWindowTransient<GroupUpdateViewModel, GroupUpdateWindow>()
            .AddWindowTransient<GroupDeleteViewModel, GroupDeleteWindow>()
            .AddWindowTransient<StudentAddViewModel, StudentAddWindow>()
            .AddWindowTransient<StudentUpdateViewModel, StudentUpdateWindow>()
            .AddWindowTransient<StudentDeleteViewModel, StudentDeleteWindow>()
            ;
    }
}
