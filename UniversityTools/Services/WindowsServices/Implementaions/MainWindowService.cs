using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityTool.Domain.Services;
using UniversityTool.Services.WindowsServices.Implementaions.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices.Implementaions
{
    internal class MainWindowService : BaseWindowService<MainWindow>, IMainWindowService
    {
        protected override MainWindow? Window { get; set; }

        public MainWindowService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void CloseWindow()
        {
            if (Window is { } window)
            {
                window.Close();
                return;
            }

            window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Closed += (_, _) => Window = null;

            Window = window;
            window.Close();
        }

        public override void OpenWindow()
        {
            if (Window is { } window)
            {
                // Task.Delay(1700).Wait();       // to see the main window with full tree when app is starting.
                window.Show();                    
                return;                           
            }                                     
                                                  
            window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Closed += (_, _) => Window = null;
                                                  
            Window = window;                      
            // Task.Delay(1700).Wait();           // to see the main window with full tree when app is starting.
            window.Show();
        }
    }
}
