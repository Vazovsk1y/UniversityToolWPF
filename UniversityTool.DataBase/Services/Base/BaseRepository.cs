using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UniversityTool.DataBase.Context;
using UniversityTool.DataBase.Factory;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Services.DataServices.Base;

namespace UniversityTool.DataBase.Services.Base
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly UniversityToolDbContextFactory _contextFactory;

        public BaseRepository(UniversityToolDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Add(T entity)
        {
            using (UniversityToolDBContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> result = await context.Set<T>().AddAsync(entity).ConfigureAwait(false);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (UniversityToolDBContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(int id)
        {
            using (UniversityToolDBContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (UniversityToolDBContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<T>().ToListAsync().ConfigureAwait(false);
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (UniversityToolDBContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
