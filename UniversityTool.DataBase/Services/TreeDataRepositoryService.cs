using Microsoft.EntityFrameworkCore;
using UniversityTool.DataBase.Context;
using UniversityTool.DataBase.Factory;
using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.DataBase.Services
{
    internal class TreeDataRepositoryService : BaseDataRepositoryService<Departament>, ITreeDataRepositoryService
    {
        public TreeDataRepositoryService(UniversityToolDbContextFactory contextFactory) : base(contextFactory) { }

        public async Task<IEnumerable<Departament>> GetFullTree()
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
