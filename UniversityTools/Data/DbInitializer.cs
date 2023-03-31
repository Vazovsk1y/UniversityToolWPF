using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UniversityTool.DataBase.Context;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.Data
{
    internal class DbInitializer : IDbInitializer
    {
        private readonly ILogger<DbInitializer> _logger;
        private readonly UniversityToolDbContext _dbContext;

        public DbInitializer(ILogger<DbInitializer> logger, UniversityToolDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task InitializeDataBaseAsync()
        {
            var timer = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation("-----Start migration process------");
                await _dbContext.Database.MigrateAsync().ConfigureAwait(false);
                _logger.LogInformation($"------Times used {timer.Elapsed.TotalSeconds}-------");
            }
            catch(OperationCanceledException ex)
            {
                _logger.LogInformation($"Exception sourse: {ex.Source}, Exception stacktrace {ex.StackTrace}, Exeption message {ex.Message}");
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Exception sourse: {ex.Source}, Exception stacktrace {ex.StackTrace}, Exeption message {ex.Message}");
            }
        }
    }
}
