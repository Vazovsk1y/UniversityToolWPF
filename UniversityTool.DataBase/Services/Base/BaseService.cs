using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UniversityTool.DataBase.Response;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.DataBase.Properties;
using UniversityTool.Domain.Response;

namespace UniversityTool.DataBase.Services.Base
{
    internal abstract class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        protected readonly IBaseRepository<T> _repository;
        protected T Item { get; set; }
        protected IEnumerable<T> Items { get; set; }

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async virtual Task<ISingleDataResponse<T>> Add(T entityToAdd)
        {
            try
            {
               Item = await _repository.Add(entityToAdd).ConfigureAwait(false);

                if (Item is null)
                {
                    return CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Item);
                }
            }
            catch(DbUpdateException ex)
            {
                Debug.WriteLine(ex);
                return CreateResponce(Resources.NullParametrErrorMessage, StatusCode.Fail, Item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Item);
            }

            return CreateResponce(Resources.SuccessMessage, StatusCode.Success, Item);
        }

        public async virtual Task<ICollectionDataResponse<T>> GetAll()
        {
            try
            {
                Items = await _repository.GetAll().ConfigureAwait(false);

                if (Items is null)
                {
                    return CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Items);
                }
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex);
                return CreateResponce(Resources.NullParametrErrorMessage, StatusCode.Fail, Items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Items);
            }

            return CreateResponce(Resources.SuccessMessage, StatusCode.Success, Items);
        }

        protected virtual ISingleDataResponse<T> CreateResponce(string description, StatusCode statusCode, T data)
            => new SingleDataResponse<T>
            {
                Description = description,
                StatusCode = statusCode,
                Data = data
            };

        protected virtual ICollectionDataResponse<T> CreateResponce(string description, StatusCode statusCode, IEnumerable<T> data)
            => new CollectionDataResponse<T>
            {
                Description = description,
                StatusCode = statusCode,
                Data = data
            };
    }
}
