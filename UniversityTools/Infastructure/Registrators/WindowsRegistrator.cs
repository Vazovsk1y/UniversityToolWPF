using Microsoft.Extensions.DependencyInjection;
using UniversityTool.Infastructure.Registrators.Base;
using UniversityTool.ViewModels;
using UniversityTool.ViewModels.AddingViemModels;
using UniversityTool.ViewModels.DeletingVIewModels;
using UniversityTool.ViewModels.UpdatingViewModels;
using UniversityTool.Views.Windows;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class WindowsRegistrator
    {
        public static IServiceCollection AddWindows(this IServiceCollection services) => services
            .AddScoped(
                s =>
                {
                    var scope = s.CreateScope();
                    var viewModel = scope.ServiceProvider.GetRequiredService<MainWindowViewModel>();
                    var window = new MainWindow { DataContext = viewModel };
                    window.Closed += (_, _) => scope.Dispose();

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
            ;
    }
}
