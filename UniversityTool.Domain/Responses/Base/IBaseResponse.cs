using UniversityTool.Domain.Codes;

namespace UniversityTool.Domain.Responses.Base
{
    public interface IBaseResponse<T>
    {
        string Description { get; set; }

        OperationStatusCode StatusCode { get; set; }
    }
}
