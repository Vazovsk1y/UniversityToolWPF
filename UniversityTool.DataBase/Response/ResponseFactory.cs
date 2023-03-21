using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Response;

namespace UniversityTool.DataBase.Response
{
    internal class ResponseFactory<T> : IResponseFactory<T> where T : BaseModel
    {
        public ICollectionDataResponse<T> CreateResponce(string description, StatusCode statusCode, IEnumerable<T>? Data) =>
            new CollectionDataResponse<T>
            {
                Description = description,
                StatusCode = statusCode,
                Data = Data
            };

        public ISingleDataResponse<T> CreateResponce(string description, StatusCode statusCode, T? Data) =>
            new SingleDataResponse<T>
            {
                Description = description,
                StatusCode = statusCode,
                Data = Data
            };
    }
}
