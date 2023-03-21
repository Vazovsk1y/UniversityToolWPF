using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Responses;

namespace UniversityTool.DataBase.Responses
{
    internal class ResponseFactory<T> : IResponseFactory<T> where T : BaseModel
    {
        public ICollectionDataResponse<T> CreateResponce(string description, OperationStatusCode statusCode, IEnumerable<T>? Data) =>
            new CollectionDataResponse<T>
            {
                Description = description,
                StatusCode = statusCode,
                Data = Data
            };

        public ISingleDataResponse<T> CreateResponce(string description, OperationStatusCode statusCode, T? Data) =>
            new SingleDataResponse<T>
            {
                Description = description,
                StatusCode = statusCode,
                Data = Data
            };
    }
}
