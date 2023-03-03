using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UniversityTool.Services.Implementaions;
using UniversityTool.Services;
using UniversityTool.ViewModels;
using UniversityTool.Views.Windows;

namespace UniversityTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider? _services;

        public static IServiceProvider Services => _services ??= InitializeServices().BuildServiceProvider();

        // all app services must be register in this method
        private static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<MainWindowViewModel>();
            services.AddScoped<DepartamentAddViewModel>();
            services.AddSingleton<MainWindowService>();                               // only for startup method
            services.AddSingleton<IDepartamentAddService, DepartamentAddService>();
            services.AddSingleton<IMessageBus, MessageBusService>();

            services.AddTransient(
                s =>
                {
                    var model = s.GetRequiredService<MainWindowViewModel>();
                    var window = new MainWindow { DataContext = model };
                    return window;
                });

            services.AddTransient(
                s =>
                {
                    var scope = s.CreateScope();
                    var model = scope.ServiceProvider.GetRequiredService<DepartamentAddViewModel>();
                    var window = new DepartamentAddWindow { DataContext = model };
                    window.Closed += (_, _) => scope.Dispose();

                    return window;
                });

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Services.GetRequiredService<MainWindowService>().OpenWindow();
        }
    }
}
