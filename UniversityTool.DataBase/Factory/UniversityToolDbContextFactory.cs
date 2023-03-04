using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UniversityTool.DataBase.Context;

namespace UniversityTool.DataBase.Factory
{
    public class UniversityToolDbContextFactory : IDesignTimeDbContextFactory<UniversityToolDBContext>
    {
        public UniversityToolDBContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<UniversityToolDBContext>();
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UniversityToolDB;Trusted_Connection=True;");

            return new UniversityToolDBContext(options.Options);
        }
    }
}
