using Microsoft.Extensions.DependencyInjection;
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
            .AddTransient(
                s =>
                {
                    var viewModel = s.GetRequiredService<DepartamentAddViewModel>();
                    var window = new DepartamentAddWindow { DataContext = viewModel };

                    return window;
                })
            .AddTransient(
                s =>
                {
                    var viewModel = s.GetRequiredService<GroupAddViewModel>();
                    var window = new GroupAddWindow { DataContext = viewModel };

                    return window;
                })
            .AddTransient(
                s =>
                {
                    var viewModel = s.GetRequiredService<StudentAddViewModel>();
                    var window = new StudentAddWindow { DataContext = viewModel };
    
                    return window;
                })
            .AddTransient(
                s =>
                {
                    var viewModel = s.GetRequiredService<DepartamentUpdateViewModel>();
                    var window = new DepartamentUpdateWindow { DataContext = viewModel };

                    return window;
                })
            .AddTransient(
                s =>
                {
                    var viewModel = s.GetRequiredService<GroupUpdateViewModel>();
                    var window = new GroupUpdateWindow { DataContext = viewModel };

                    return window;
                })
            .AddTransient(
                s =>
                {
                    var viewModel = s.GetRequiredService<StudentUpdateViewModel>();
                    var window = new StudentUpdateWindow { DataContext = viewModel };

                    return window;
                })
            .AddTransient(
                s =>
                {
                    var viewModel = s.GetRequiredService<DepartamentDeleteViewModel>();
                    var window = new DepartamentDeleteWindow { DataContext = viewModel };

                    return window;
                })
            .AddTransient(
                s =>
                {
                    var viewModel = s.GetRequiredService<GroupDeleteViewModel>();
                    var window = new GroupDeleteWindow { DataContext = viewModel };

                    return window;
                })
                ;
    }
}
