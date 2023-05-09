using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Responses;

namespace UniversityTool.Domain.Services.DataServices.Base
{
    public interface IBaseService<T> where T : BaseModel
    {
        Task<IDataResponse<T>> Add(T entity);

        Task<IDataResponse<T>> Update(T entity);

        Task<IDataResponse<T>> Delete(T entity);

        Task<IDataResponse<IEnumerable<T>>> GetAll();
    }
}
