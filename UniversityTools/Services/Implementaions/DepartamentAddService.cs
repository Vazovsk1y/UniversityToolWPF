using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityTool.Domain.Services;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.Implementaions
{
    internal class DepartamentAddService : IDepartamentAddService
    {
        private IServiceProvider _serviceProvider;
        private DepartamentAddWindow? _mainWindow;

        public DepartamentAddService(IServiceProvider serviceProvider)
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

            window = _serviceProvider.GetRequiredService<DepartamentAddWindow>();
            window.Closed += (_, _) => _mainWindow = null;

            _mainWindow = window;
            window.Close();
        }

        public void OpenWindow()
        {
            if (_mainWindow is { } window)
            {
                window.ShowDialog();
                return;
            }

            window = _serviceProvider.GetRequiredService<DepartamentAddWindow>();
            window.Closed += (_, _) => _mainWindow = null;

            _mainWindow = window;
            window.ShowDialog();
        }
    }
}
