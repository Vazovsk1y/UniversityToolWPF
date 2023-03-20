using UniversityTool.Domain.Codes;

namespace UniversityTool.Domain.Response.Base
{
    public interface IBaseResponse<T>
    {
        string Description { get; set; }

        StatusCode StatusCode { get; set; }
    }
}
