﻿using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;

namespace UniversityTool.Domain.Responses
{
    public interface IResponseFactory<T> where T : BaseModel 
    {
        //public ICollectionDataResponse<T> CreateResponce(string description, OperationResultStatusCode statusCode, IEnumerable<T> Data);
        public IDataResponse<T> CreateResponce(string description, OperationResultStatusCode statusCode, T Data);
        public IDataResponse<IEnumerable<T>> CreateResponce(string description, OperationResultStatusCode statusCode, IEnumerable<T> Data);
    }
}
