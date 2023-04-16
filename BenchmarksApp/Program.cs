using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Registrators.Repositories;
using UniversityTool.DataBase.Registrators.Services;

namespace BenchmarksApp
{
    public class Test
    {
        
    }

    internal class Program
    {
        private static IServiceProvider? _service;

        public static IServiceProvider Service => _service ??= InitializeServices().BuildServiceProvider();

        private static IServiceCollection InitializeServices() => new ServiceCollection()
            .AddDbServices()
            .AddDbRepositories()
            .AddTransient<Test>()
            ;

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Test>();
        }
    }
}