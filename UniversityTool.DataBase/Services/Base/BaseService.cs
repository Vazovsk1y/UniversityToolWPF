using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.DataBase.Properties;
using UniversityTool.Domain.Responses;
using Microsoft.Extensions.Logging;

namespace UniversityTool.DataBase.Services.Base
{
    internal abstract class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        protected readonly ILogger<BaseService<T>> _logger;
        protected readonly IBaseRepository<T> _repository;
        protected readonly IResponseFactory<T> _responseFactory;
        protected T Item { get; set; }
        protected IEnumerable<T> Items { get; set; }

        public BaseService(IBaseRepository<T> repository, 
            IResponseFactory<T> responceFactory, 
            ILogger<BaseService<T>> logger)
        {
            _logger = logger;
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
               _logger.LogInformation($"Adding {entityToAdd.GetType()} is started.");
               Item = await _repository.Add(entityToAdd).ConfigureAwait(false);

                if (Item is null)
                {
                    _logger.LogInformation($"Entity is not exist in database");
                    return _responseFactory.CreateResponce(Resources.AddingErrorMessage, OperationResultStatusCode.Fail, Item);
                }
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError($"Exception thrown {ex.Message}");
                return _responseFactory.CreateResponce(Resources.NullParametrErrorMessage + Resources.AddingErrorMessage, OperationResultStatusCode.Fail, Item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown {ex.Message}");
                return _responseFactory.CreateResponce(Resources.AddingErrorMessage, OperationResultStatusCode.Fail, Item);
            }

            _logger.LogInformation($"Operation succsesfully completed.");
            return _responseFactory.CreateResponce(Resources.AddingSuccessMessage, OperationResultStatusCode.Success, Item);
        }

        public async virtual Task<ICollectionDataResponse<T>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Getting all entities is started.");
                Items = await _repository.GetAll().ConfigureAwait(false);

                if (Items is null)
                {
                    _logger.LogInformation($"Entities was null");
                    return _responseFactory.CreateResponce(string.Empty, OperationResultStatusCode.Fail, Items);
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Exception thrown {ex.Message}");
                return _responseFactory.CreateResponce(string.Empty, OperationResultStatusCode.Fail, Items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown {ex.Message}");
                return _responseFactory.CreateResponce(string.Empty, OperationResultStatusCode.Fail, Items);
            }

            _logger.LogInformation($"Operation succsesfully completed.");
            return _responseFactory.CreateResponce(Resources.AddingSuccessMessage, OperationResultStatusCode.Success, Items);
        }

        public async Task<ISingleDataResponse<T>> Update(T entity)
        {
            try
            {
                _logger.LogInformation($"Updating {entity.GetType()} is started");
                Item = await _repository.Update(entity).ConfigureAwait(false);

                if (Item is null)
                {
                    _logger.LogInformation($"Entity is not exist in database");
                    return _responseFactory.CreateResponce(Resources.UpdateErrorMessage, OperationResultStatusCode.Fail, Item);
                }
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError($"Exception thrown {ex.Message}");
                return _responseFactory.CreateResponce(Resources.NullParametrErrorMessage + Resources.UpdateErrorMessage, OperationResultStatusCode.Fail, Item);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception thrown {ex.Message}");
                return _responseFactory.CreateResponce(Resources.UpdateErrorMessage, OperationResultStatusCode.Fail, Item);
            }

            _logger.LogInformation($"Operation succsesfully completed.");
            return _responseFactory.CreateResponce(Resources.UpdateSuccessMessage, OperationResultStatusCode.Success, Item);
        }

        public async Task<ISingleDataResponse<T>> Delete(T entity)
        {
            try
            {
                _logger.LogInformation($"Deleting {entity.GetType()} is started");
                Item = await _repository.Delete(entity.Id).ConfigureAwait(false);

                if (Item is null)
                {
                    _logger.LogInformation($"Entity is not exist in database");
                    return _responseFactory.CreateResponce(Resources.DeleteErrorMessage, OperationResultStatusCode.Fail, Item);
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Exception thrown {ex.Message}");
                return _responseFactory.CreateResponce(Resources.NullParametrErrorMessage + Resources.DeleteErrorMessage, OperationResultStatusCode.Fail, Item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown {ex.Message}");
                return _responseFactory.CreateResponce(Resources.DeleteErrorMessage, OperationResultStatusCode.Fail, Item);
            }

            _logger.LogInformation($"Operation succsesfully completed.");
            return _responseFactory.CreateResponce(Resources.DeleteSuccessMessage, OperationResultStatusCode.Success, Item);
        }
    }
}
