using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityTool.Domain.Services;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.Implementaions
{
    internal class MainWindowService : IMainWindowService
    {
        private IServiceProvider _serviceProvider;
        private MainWindow? _mainWindow;

        public MainWindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CloseWindow()
        {
            if (_mainWindow is { } window)
            {
                window.Close();
                return;
            }

            window = _serviceProvider.GetRequiredService<MainWindow>();
            window.Closed += (_, _) => _mainWindow = null;

            _mainWindow = window;
            window.Close();
        }

        public void OpenWindow()
        {
            if (_mainWindow is { } window)
            {
                window.Show();
                return;
            }

            window = _serviceProvider.GetRequiredService<MainWindow>();
            window.Closed += (_, _) => _mainWindow = null;

            _mainWindow = window;
            window.Show();
        }
    }
}
