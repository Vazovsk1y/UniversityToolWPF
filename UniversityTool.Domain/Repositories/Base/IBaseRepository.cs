namespace UniversityTool.Domain.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(CancellationToken token = default);

        Task<T> Get(int id, CancellationToken token = default);

        Task<T> Add(T entity, CancellationToken token = default);

        Task<T> Update(T entity, CancellationToken token = default);

        Task<T> Delete(int id, CancellationToken token = default);
    }
}
