using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UniversityTool.DataBase.Context;

namespace UniversityTool.DataBase.Factory
{
    public class UniversityToolDbContextFactory : IDesignTimeDbContextFactory<UniversityToolDbContext>
    {
        public UniversityToolDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<UniversityToolDbContext>();
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UniversityToolDB;Trusted_Connection=True;");

            return new UniversityToolDbContext(options.Options);
        }
    }
}
