using UniversityTool.Domain.Responses.Base;

namespace UniversityTool.Domain.Responses
{
    public interface IDataResponse<T> : IBaseResponse<T>
    {
        T Data { get; set; }
    }
}
