using UniversityTool.DataBase.Responses.Base;
using UniversityTool.Domain.Responses;

namespace UniversityTool.DataBase.Responses
{
    internal class DataResponse<T> : BaseResponse<T>, IDataResponse<T>
    {
        public T Data { get; set; }
    }
}
