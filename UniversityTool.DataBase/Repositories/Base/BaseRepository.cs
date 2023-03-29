﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UniversityTool.DataBase.Context;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Repositories.Base;

namespace UniversityTool.DataBase.Repositories.Base
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly IDbContextFactory<UniversityToolDbContext> _contextFactory;

        public BaseRepository(IDbContextFactory<UniversityToolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Add(T entity, CancellationToken token = default)
        {
            try
            {
                await using UniversityToolDbContext context = _contextFactory.CreateDbContext();
                EntityEntry<T> result = await context.Set<T>().AddAsync(entity, token).ConfigureAwait(false);
                await context.SaveChangesAsync(token);
                return result.Entity;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id, CancellationToken token = default)
        {
            try
            {
                await using UniversityToolDbContext context = _contextFactory.CreateDbContext();
                T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, token).ConfigureAwait(false);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync(token);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Get(int id, CancellationToken token = default)
        {
            try
            {
                await using UniversityToolDbContext context = _contextFactory.CreateDbContext();
                return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, token).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken token = default)
        {
            try
            {
                await using UniversityToolDbContext context = _contextFactory.CreateDbContext();
                return await context.Set<T>().ToListAsync(token).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Update(T entity, CancellationToken token = default)
        {
            try
            {
                await using UniversityToolDbContext context = _contextFactory.CreateDbContext();
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync(token);
                return entity;
            }
            catch
            {
                throw;
            }
        }
    }
}
