using Microsoft.Extensions.DependencyInjection;
using UniversityTool.ViewModels;
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
                    var model = scope.ServiceProvider.GetRequiredService<MainWindowViewModel>();
                    var window = new MainWindow { DataContext = model };
                    window.Closed += (_, _) => scope.Dispose();

                    //var model = s.GetRequiredService<MainWindowViewModel>();
                    //var window = new MainWindow { DataContext = model };

                    return window;
                })
            .AddTransient(
                s =>
                {
                    var model = s.GetRequiredService<DepartamentAddViewModel>();
                    var window = new DepartamentAddWindow { DataContext = model };

                    return window;
                })
            .AddTransient(
                s =>
                {
                    var model = s.GetRequiredService<GroupAddViewModel>();
                    var window = new GroupAddWindow { DataContext = model };

                    return window;
                })
            ;
    }
}
