using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Response;

namespace UniversityTool.Domain.Services.DataServices.Base
{
    public interface IBaseService<T> where T : BaseModel
    {
        Task<ISingleDataResponse<T>> Add(T entity);

        Task<ICollectionDataResponse<T>> GetAll();
    }
}
