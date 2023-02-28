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

        private static ServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindowService>();               // for startUp method
            services.AddSingleton<IUserDialog, MainWindowService>();
            services.AddSingleton<IMessageBus, MessageBusService>();

            services.AddTransient(
                s =>
                {
                    var model = s.GetRequiredService<MainWindowViewModel>();
                    var window = new MainWindow { DataContext = model };
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
