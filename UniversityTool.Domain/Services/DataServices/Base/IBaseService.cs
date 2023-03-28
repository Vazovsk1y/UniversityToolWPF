using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Responses;

namespace UniversityTool.Domain.Services.DataServices.Base
{
    public interface IBaseService<T> where T : BaseModel
    {
        Task<ISingleDataResponse<T>> Add(T entity);

        Task<ISingleDataResponse<T>> Update(T entity);

        Task<ICollectionDataResponse<T>> GetAll();
    }
}
