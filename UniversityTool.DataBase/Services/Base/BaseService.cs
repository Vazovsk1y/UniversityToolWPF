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
        protected readonly IResponseFactory<T> _responseFactory;
        protected T Item { get; set; }
        protected IEnumerable<T> Items { get; set; }

        public BaseService(IBaseRepository<T> repository, IResponseFactory<T> responceFactory)
        {
            _repository = repository;
            _responseFactory = responceFactory;
        }

        public async virtual Task<ISingleDataResponse<T>> Add(T entityToAdd)
        {
            try
            {
               Item = await _repository.Add(entityToAdd).ConfigureAwait(false);

                if (Item is null)
                {
                    return _responseFactory.CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Item);
                }
            }
            catch(DbUpdateException ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.NullParametrErrorMessage, StatusCode.Fail, Item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Item);
            }

            return _responseFactory.CreateResponce(Resources.SuccessMessage, StatusCode.Success, Item);
        }

        public async virtual Task<ICollectionDataResponse<T>> GetAll()
        {
            try
            {
                Items = await _repository.GetAll().ConfigureAwait(false);

                if (Items is null)
                {
                    return _responseFactory.CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Items);
                }
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.NullParametrErrorMessage, StatusCode.Fail, Items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Items);
            }

            return _responseFactory.CreateResponce(Resources.SuccessMessage, StatusCode.Success, Items);
        }
    }
}
