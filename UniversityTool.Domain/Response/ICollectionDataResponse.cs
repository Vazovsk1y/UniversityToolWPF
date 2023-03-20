using UniversityTool.Domain.Response.Base;

namespace UniversityTool.Domain.Response
{
    public interface ICollectionDataResponse<T> : IBaseResponse<T>
    {
        IEnumerable<T> Data { get; set; }
    }
}
