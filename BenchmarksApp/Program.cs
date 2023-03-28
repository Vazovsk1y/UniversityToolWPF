using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniversityTool.DataBase.Context;
using UniversityTool.DataBase.Registrators.Repositories;
using UniversityTool.DataBase.Registrators.Services;
using UniversityTool.DataBase.Repositories;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Repositories;
using UniversityTool.Domain.Services.DataServices;

namespace BenchmarksApp
{
    public class Test
    {
        private readonly IDepartamentTreeService _service;

        public Test()
        {
            _service = Program.Service.GetRequiredService<IDepartamentTreeService>();  
        }

        [Benchmark(Baseline =true)]
        public async Task InitializeFullTreeAsync() => await Task.Run(async () =>
        {
            var response = await _service.GetFullDepartamentsTree().ConfigureAwait(false);
            if (response.StatusCode == OperationResultStatusCode.Success)
            {
                Task.Run(() => { var data = response.Data; });
            }
        });

        [Benchmark]
        public async Task InitializeSecond()
        {
            var response = await _service.GetFullDepartamentsTree().ConfigureAwait(false);
            if (response.StatusCode == OperationResultStatusCode.Success)
            {
                Task.Run(() => { var data = response.Data; });
            }
        }
    }

    internal class Program
    {
        private static IServiceProvider? _service;

        public static IServiceProvider Service => _service ??= InitializeServices().BuildServiceProvider();

        private static IServiceCollection InitializeServices() => new ServiceCollection()
            .AddDbServices()
            .AddDbRepositories()
            .AddTransient<Test>()
            .AddDbContextFactory<UniversityToolDbContext>(o =>
            {
                o.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UniversityToolDB;Trusted_Connection=True;");
            });

        public static void Main(string[] args)
        {
            
            BenchmarkRunner.Run<Test>();
        }
    }
}