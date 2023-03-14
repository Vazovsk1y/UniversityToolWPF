﻿namespace UniversityTool.Domain.Services.DataServices.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task<T> Add(T entity);

        Task<T> Update(int id, T entity);

        Task<bool> Delete(int id);
    }
}
