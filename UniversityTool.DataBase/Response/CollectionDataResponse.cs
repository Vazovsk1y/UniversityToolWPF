using UniversityTool.DataBase.Response.Base;
using UniversityTool.Domain.Response;

namespace UniversityTool.DataBase.Response
{
    internal class CollectionDataResponse<T> : BaseResponse<T>, ICollectionDataResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
    }
}
