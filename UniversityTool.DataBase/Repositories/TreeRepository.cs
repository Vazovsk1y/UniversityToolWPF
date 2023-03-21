using Microsoft.EntityFrameworkCore;
using UniversityTool.DataBase.Context;
using UniversityTool.DataBase.Factory;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories;

namespace UniversityTool.DataBase.Repositories
{
    internal class TreeRepository : ITreeRepository
    {
        private readonly UniversityToolDbContextFactory _contextFactory;

        public TreeRepository(UniversityToolDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Departament>> GetDepartamentsRelations()
        {
            using (UniversityToolDBContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Departament> departaments =
                    await context.Departaments
                    .Include(d => d.Groups)
                    .ThenInclude(g => g.Students)
                    .ToListAsync().ConfigureAwait(false);

                return departaments;
            }
        }
    }
}
