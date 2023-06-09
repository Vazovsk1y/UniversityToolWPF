﻿using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityTool.Domain.Services;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class MainWindowService : BaseWindowService<MainWindow>, IMainWindowService
    {
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
                window.Show();
                return;
            }

            window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Closed += (_, _) => Window = null;

            Window = window;
            window.Show();
        }
    }
}
