using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Windows;
using UniversityTool.Domain.Services.Base;

namespace UniversityTool.Services.WindowsServices.Base
{
    internal abstract class BaseWindowService<T> : IBaseWindowService where T : Window
    {
        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider
        {
            get => _serviceProvider;
            private set => _serviceProvider = value;
        }

        protected T? Window { get; set; }

        protected BaseWindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual void CloseWindow()
        {
            if (Window is { } window)
            {
#if DEBUG
                Debug.WriteLine($"{GetHashCode()} WINDOW SERVICE");
                Debug.WriteLine($"{window.GetHashCode()} WINDOW with type {typeof(T)} CLOSING by METHOD.");
#endif
                window.Close();
                return;
            }

            //throw new InvalidOperationException($"{typeof(T)} Window wasn't opened");
            var scope = ServiceProvider.CreateScope();
            window = scope.ServiceProvider.GetRequiredService<T>();

            window.Closed += (_, _) =>
            {
                Window = null;
                scope.Dispose();
            };

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

            var scope = ServiceProvider.CreateScope();
            window = scope.ServiceProvider.GetRequiredService<T>();
#if DEBUG
            Debug.WriteLine($"{GetHashCode()} WINDOW SERVICE");
            Debug.WriteLine($"{window.GetHashCode()} WINDOW with type {typeof(T)} OPENING.");
#endif
            window.Closed += (_, _) =>
            {
#if DEBUG
                Debug.WriteLine($"{GetHashCode()} WINDOW SERVICE");
                Debug.WriteLine($"{window.GetHashCode()} WINDOW with type {typeof(T)} CLOSING by PRESSING X.");
#endif
                Window = null;
                scope.Dispose();
            };

            Window = window;
            window.ShowDialog();
        }
    }
}
