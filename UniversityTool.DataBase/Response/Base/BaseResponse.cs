using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Response.Base;

namespace UniversityTool.DataBase.Response.Base
{
    internal abstract class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
    }
}
