using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UniversityTool.Domain.Services;
using System.Threading;
using UniversityTool.Infastructure.Registrators;
using Microsoft.Extensions.Hosting;
using UniversityTool.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityTool.Data;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region --Fields--

        private static IHost? _host;

        private static readonly string UniqueEventName = "UniversityTool";

        #endregion

        #region --Properties--

        public static bool IsDesignMode { get; set; } = true;

        public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        #endregion

        #region --Constructors--



        #endregion

        #region --Methods--

        protected override async void OnStartup(StartupEventArgs e)
        {
            if (IsNewInstance())
            {
                EventWaitHandle eventWaitHandle = new(false, EventResetMode.AutoReset, UniqueEventName);
                Current.Exit += (sender, args) => eventWaitHandle.Close();
                var host = Host;
                base.OnStartup(e);
                await host.StartAsync();
                IsDesignMode = false;

                using var scope = Services.CreateScope();
                await scope.ServiceProvider.GetRequiredService<IDbInitializer>().InitializeDataBaseAsync();

                Services.GetRequiredService<IMainWindowService>().OpenWindow();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Host;
            base.OnExit(e);
            await host.StopAsync();
            Services.GetRequiredService<IMainWindowService>().CloseWindow();
            Current.Shutdown();
        }

        private bool IsNewInstance()
        {
            try
            {
                EventWaitHandle eventWaitHandle = EventWaitHandle.OpenExisting(UniqueEventName); // here will be exception if app is not even starting
                eventWaitHandle.Set();
                Shutdown();
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                return true;
            }
            return false;
        }

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddAppServices()
            .AddDataBaseTools(host.Configuration.GetSection("Database"))
            .AddViewModels()
            .AddWindows()
            ;

        #endregion

        #region --#2 Way App Start--

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    if (!InstanceCheck())
        //    {
        //        MessageBox.Show("IsAlreadyStarted!");
        //        Current.Shutdown();
        //        return;
        //    }
        //    Services.GetRequiredService<IMainWindowService>().OpenWindow();
        //}

        //protected override void OnExit(ExitEventArgs e)
        //{
        //    base.OnExit(e);
        //    Services.GetRequiredService<IMainWindowService>().CloseWindow();
        //    Current.Shutdown();
        //}

        //private static Mutex? InstanceCheckMutex;
        //private static bool InstanceCheck()
        //{
        //    var mutex = new Mutex(true, "UniversityTool", out bool isNew);
        //    if (isNew)
        //        InstanceCheckMutex = mutex;
        //    else
        //        mutex.Dispose();

        //    return isNew;
        //}

        #endregion
    }
}
