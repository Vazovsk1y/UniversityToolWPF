using Microsoft.EntityFrameworkCore;
using UniversityTool.DataBase.Context;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories;

namespace UniversityTool.DataBase.Repositories
{
    internal class TreeRepository : ITreeRepository
    {
        protected readonly IDbContextFactory<UniversityToolDbContext> _contextFactory;

        public TreeRepository(IDbContextFactory<UniversityToolDbContext>  contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Departament>> GetDepartamentsRelations()
        {
            using UniversityToolDbContext context = _contextFactory.CreateDbContext();
            IEnumerable<Departament> departaments =
                await context.Departaments
                .Include(d => d.Groups)
                .ThenInclude(g => g.Students)
                .ToListAsync().ConfigureAwait(false);

            return departaments;
        }
    }
}
