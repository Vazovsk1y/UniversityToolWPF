using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.Infastructure.Registrators.Base
{
    internal static class GenericWindowRegistrator
    {
        public static IServiceCollection AddWindowTransient<TViewModel, TWindow>(this IServiceCollection services) 
            where TViewModel : BaseViewModel
            where TWindow : Window
            => services.AddTransient(
            s =>
            {
                var viewModel = s.GetRequiredService<TViewModel>();
                var window = Activator.CreateInstance<TWindow>();
                window.DataContext = viewModel;
                return window;
            });
    }
}
