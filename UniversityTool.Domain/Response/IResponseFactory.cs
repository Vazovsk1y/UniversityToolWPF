using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Response
{
    public interface IResponseFactory<T> where T : BaseModel 
    {
        public ICollectionDataResponse<T> CreateResponce(string description, StatusCode statusCode, IEnumerable<T> Data);
        public ISingleDataResponse<T> CreateResponce(string description, StatusCode statusCode, T Data);
    }
}
