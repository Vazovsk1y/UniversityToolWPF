using UniversityTool.Domain.Responses.Base;

namespace UniversityTool.Domain.Responses
{
    public interface ISingleDataResponse<T> : IBaseResponse<T>
    {
        T Data { get; set; }
    }
}
