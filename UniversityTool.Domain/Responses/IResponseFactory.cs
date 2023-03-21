using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Responses
{
    public interface IResponseFactory<T> where T : BaseModel 
    {
        public ICollectionDataResponse<T> CreateResponce(string description, OperationStatusCode statusCode, IEnumerable<T> Data);
        public ISingleDataResponse<T> CreateResponce(string description, OperationStatusCode statusCode, T Data);
    }
}
