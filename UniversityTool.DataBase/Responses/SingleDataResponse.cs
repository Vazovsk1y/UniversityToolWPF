using UniversityTool.DataBase.Responses.Base;
using UniversityTool.Domain.Responses;

namespace UniversityTool.DataBase.Responses
{
    internal class SingleDataResponse<T> : BaseResponse<T>, ISingleDataResponse<T>
    {
        public T Data { get; set; }
    }
}
