using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Responses.Base;

namespace UniversityTool.DataBase.Responses.Base
{
    internal abstract class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public OperationStatusCode StatusCode { get; set; }
    }
}
