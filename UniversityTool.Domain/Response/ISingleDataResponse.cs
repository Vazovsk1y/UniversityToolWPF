using UniversityTool.Domain.Response.Base;

namespace UniversityTool.Domain.Response
{
    public interface ISingleDataResponse<T> : IBaseResponse<T>
    {
        T Data { get; set; }
    }
}
