using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels;
using UniversityTool.Views.Windows;
using UniversityTool.Services.WindowsServices.Implementaions;
using UniversityTool.Services.DataServices.Impementations;
using UniversityTool.DataBase.Factory;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.DataBase.Services;
using UniversityTool.Domain.Services.WindowsServices;
using System.Threading;

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

            #region --DataBase, ViewModel, Services--

            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<DepartamentAddViewModel>();
            services.AddTransient<GroupAddViewModel>();
            services.AddSingleton<IMainWindowService, MainWindowService>();                               // only for startup method
            services.AddSingleton<IDepartamentAddWindowService, DepartamentAddWindowService>();
            services.AddSingleton<IGroupAddWindowService, GroupAddWindowService>();
            services.AddSingleton<IMessageBusService, MessageBusService>();
            services.AddScoped<UniversityToolDbContextFactory>();
            services.AddScoped(typeof(IDataService<>), typeof(DataService<>));

            #endregion

            #region --Windows--

            services.AddTransient(
                s =>
                {
                    //var scope = s.CreateScope();
                    //var model = scope.ServiceProvider.GetRequiredService<MainWindowViewModel>();
                    //window.Closed += (_, _) => scope.Dispose();
                    //var window = new MainWindow { DataContext = model };

                    var model = s.GetRequiredService<MainWindowViewModel>();
                    var window = new MainWindow { DataContext = model };
                    

                    return window;
                });

            services.AddTransient(
                s =>
                {
                    var model = s.GetRequiredService<DepartamentAddViewModel>();
                    var window = new DepartamentAddWindow { DataContext = model };

                    return window;
                });

            services.AddTransient(
                s =>
                {
                    var model = s.GetRequiredService<GroupAddViewModel>();
                    var window = new GroupAddWindow { DataContext = model };

                    return window;
                });

            #endregion

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Services.GetRequiredService<IMainWindowService>().OpenWindow();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Services.GetRequiredService<IMainWindowService>().CloseWindow();
        }
    }
}
