using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UniversityTool.Domain.Services.Base;

namespace UniversityTool.Services.WindowsServices.Implementaions.Base
{
    internal abstract class BaseWindowService<T> : IBaseWindowService where T : Window
    {
        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider 
        { 
            get => _serviceProvider; 
            private set => _serviceProvider = value; 
        }

        protected abstract T? Window { get; set; }

        protected BaseWindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual void CloseWindow()
        {
            if (Window is { } window)
            {
                window.Close();
                return;
            }

            window = ServiceProvider.GetRequiredService<T>();
            window.Closed += (_, _) => Window = null;

            Window = window;
            window.Close();
        }

        public virtual void OpenWindow()
        {
            if (Window is { } window)
            {
                window.ShowDialog();
                return;
            }

            window = ServiceProvider.GetRequiredService<T>();
            window.Closed += (_, _) => Window = null;

            Window = window;
            window.ShowDialog();
        }
    }
}
