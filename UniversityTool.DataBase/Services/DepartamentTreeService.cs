using System.Diagnostics;
using UniversityTool.DataBase.Properties;
using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories;
using UniversityTool.Domain.Responses;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.DataBase.Services
{
    internal class DepartamentTreeService : BaseService<Departament>, IDepartamentTreeService
    {
        private readonly IDepartamentTreeRepository _departamentTreeRepository;

        public DepartamentTreeService(IDepartamentTreeRepository repository, IResponseFactory<Departament> responceFactory) : base(responceFactory)
        {
            _departamentTreeRepository = repository;
        }

        public async Task<ICollectionDataResponse<Departament>> GetFullDepartamentsTree()
        {
            try
            {
                Items = await _departamentTreeRepository.GetDepartamentsRelations().ConfigureAwait(false);

                if (Items is null)
                {
                    return _responseFactory.CreateResponce(Resources.GettingErrorMessage, OperationResultStatusCode.Fail, Items);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return _responseFactory.CreateResponce(Resources.GettingErrorMessage, OperationResultStatusCode.Fail, Items);
            }

            return _responseFactory.CreateResponce(Resources.GettingSuccessMessage, OperationResultStatusCode.Success, Items);
        }
    }
}
