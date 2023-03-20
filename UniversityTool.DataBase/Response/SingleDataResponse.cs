using UniversityTool.DataBase.Response.Base;
using UniversityTool.Domain.Response;

namespace UniversityTool.DataBase.Response
{
    internal class SingleDataResponse<T> : BaseResponse<T>, ISingleDataResponse<T>
    {
        public T Data { get; set; }
    }
}
