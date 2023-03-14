using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UniversityTool.Domain.Services;
using System.Threading;
using UniversityTool.Infastructure.Registrators;

namespace UniversityTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region --Fields--

        private static readonly string UniqueEventName = "UniversityTool";

        private static IServiceProvider? _services;

        public static IServiceProvider Services => _services ??= InitializeServices().BuildServiceProvider();

        private static IServiceCollection InitializeServices() => new ServiceCollection()
            .AddViewModels()
            .AddWindows()
            .AddDataContext()
            .AddServices()
            ;

        #endregion

        #region --Properties--

        public static bool IsDesignMode { get; set; } = true;

        #endregion

        #region --#2 Way App Start--

        protected override void OnStartup(StartupEventArgs e)
        {
            bool isNewInstance = false;
            try
            {
                EventWaitHandle eventWaitHandle = EventWaitHandle.OpenExisting(UniqueEventName); // here will be exception if app is not even starting
                eventWaitHandle.Set();
                Shutdown();
                return;
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                isNewInstance = true;
            }

            if (isNewInstance)
            {
                EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, UniqueEventName);
                Current.Exit += (sender, args) => eventWaitHandle.Close();
            }

            base.OnStartup(e);
            IsDesignMode = false;
            Services.GetRequiredService<IMainWindowService>().OpenWindow();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Services.GetRequiredService<IMainWindowService>().CloseWindow();
            Current.Shutdown();
        }

        #endregion

        #region --#1 Way App Start--

        #region --Start and Exit--

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

        #endregion

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
