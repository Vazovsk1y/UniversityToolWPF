using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityTool.Data;
using UniversityTool.DataBase.Context;
using UniversityTool.DataBase.Registrators.Repositories;
using UniversityTool.DataBase.Registrators.Services;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.Infastructure.Registrators
{
    internal static class DataBaseToolsRegistrator
    {
        public static IServiceCollection AddDataBaseTools(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbRepositories()
            .AddDbServices()
            .AddDbContextFactory<UniversityToolDbContext>(options => 
            {
                var dbType = configuration["Type"];
                switch(dbType)
                {
                    case "MSSQL":
                        {
                            options.UseSqlServer(configuration.GetConnectionString(dbType)).UseSnakeCaseNamingConvention();
                            break;
                        }

                    case null: throw new InvalidOperationException("Undefined database type");
                    default: throw new InvalidOperationException($"Database {dbType} is not supported");
                }
            })
            .AddTransient<IDbInitializer, DbInitializer>()
            ;
    }
}
