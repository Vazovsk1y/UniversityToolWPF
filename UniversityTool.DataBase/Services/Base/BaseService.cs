using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.DataBase.Properties;
using UniversityTool.Domain.Responses;

namespace UniversityTool.DataBase.Services.Base
{
    internal abstract class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        protected readonly IBaseRepository<T> _repository;
        protected readonly IResponseFactory<T> _responseFactory;
        protected T Item { get; set; }
        protected IEnumerable<T> Items { get; set; }

        public BaseService(IBaseRepository<T> repository, IResponseFactory<T> responceFactory)
        {
            _repository = repository;
            _responseFactory = responceFactory;
        }

        public BaseService(IResponseFactory<T> responceFactory)
        {
            _responseFactory = responceFactory;
        }

        public async virtual Task<ISingleDataResponse<T>> Add(T entityToAdd)
        {
            try
            {
               Item = await _repository.Add(entityToAdd).ConfigureAwait(false);

                if (Item is null)
                {
                    return _responseFactory.CreateResponce(Resources.AddingErrorMessage, OperationResultStatusCode.Fail, Item);
                }
            }
            catch(DbUpdateException ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.NullParametrErrorMessage + Resources.AddingErrorMessage, OperationResultStatusCode.Fail, Item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.AddingErrorMessage, OperationResultStatusCode.Fail, Item);
            }

            return _responseFactory.CreateResponce(Resources.AddingSuccessMessage, OperationResultStatusCode.Success, Item);
        }

        public async virtual Task<ICollectionDataResponse<T>> GetAll()
        {
            try
            {
                Items = await _repository.GetAll().ConfigureAwait(false);

                if (Items is null)
                {
                    return _responseFactory.CreateResponce(string.Empty, OperationResultStatusCode.Fail, Items);
                }
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(string.Empty, OperationResultStatusCode.Fail, Items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(string.Empty, OperationResultStatusCode.Fail, Items);
            }

            return _responseFactory.CreateResponce(Resources.AddingSuccessMessage, OperationResultStatusCode.Success, Items);
        }

        public async Task<ISingleDataResponse<T>> Update(T entity)
        {
            try
            {
                Item = await _repository.Update(entity).ConfigureAwait(false);

                if (Item is null)
                {
                    return _responseFactory.CreateResponce(Resources.UpdateErrorMessage, OperationResultStatusCode.Fail, Item);
                }
            }
            catch(DbUpdateException ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.NullParametrErrorMessage + Resources.UpdateErrorMessage, OperationResultStatusCode.Fail, Item);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.UpdateErrorMessage, OperationResultStatusCode.Fail, Item);
            }

            return _responseFactory.CreateResponce(Resources.UpdateSuccessMessage, OperationResultStatusCode.Success, Item);
        }

        public async Task<ISingleDataResponse<T>> Delete(T entity)
        {
            try
            {
                Item = await _repository.Delete(entity.Id).ConfigureAwait(false);

                if (Item is null)
                {
                    return _responseFactory.CreateResponce(Resources.DeleteErrorMessage, OperationResultStatusCode.Fail, Item);
                }
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.NullParametrErrorMessage + Resources.DeleteErrorMessage, OperationResultStatusCode.Fail, Item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.DeleteErrorMessage, OperationResultStatusCode.Fail, Item);
            }

            return _responseFactory.CreateResponce(Resources.DeleteSuccessMessage, OperationResultStatusCode.Success, Item);
        }
    }
}
