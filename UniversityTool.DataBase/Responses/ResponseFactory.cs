using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Responses;

namespace UniversityTool.DataBase.Responses
{
    internal class ResponseFactory<T> : IResponseFactory<T> where T : BaseModel
    {
        public IDataResponse<IEnumerable<T>> CreateResponce(string description, OperationResultStatusCode statusCode, IEnumerable<T>? Data) =>
            new DataResponse<IEnumerable<T>>
            {
                Description = description,
                StatusCode = statusCode,
                Data = Data
            };

        public IDataResponse<T> CreateResponce(string description, OperationResultStatusCode statusCode, T? Data) =>
            new DataResponse<T>
            {
                Description = description,
                StatusCode = statusCode,
                Data = Data
            };
    }
}
