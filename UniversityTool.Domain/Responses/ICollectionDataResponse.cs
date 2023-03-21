using UniversityTool.Domain.Responses.Base;

namespace UniversityTool.Domain.Responses
{
    public interface ICollectionDataResponse<T> : IBaseResponse<T>
    {
        IEnumerable<T> Data { get; set; }
    }
}
