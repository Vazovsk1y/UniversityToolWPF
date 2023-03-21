using UniversityTool.DataBase.Responses.Base;
using UniversityTool.Domain.Responses;

namespace UniversityTool.DataBase.Responses
{
    internal class CollectionDataResponse<T> : BaseResponse<T>, ICollectionDataResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
    }
}
