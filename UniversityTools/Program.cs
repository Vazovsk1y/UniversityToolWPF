using Microsoft.Extensions.Hosting;
using System;

namespace UniversityTool
{
    internal class Program
    {
        [STAThread]
        internal static void Main(string[] args)
        {
            App app = new();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(App.ConfigureServices)
            ;
    }
}
