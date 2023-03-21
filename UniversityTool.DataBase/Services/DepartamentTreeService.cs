using System.Diagnostics;
using UniversityTool.DataBase.Properties;
using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories;
using UniversityTool.Domain.Response;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.DataBase.Services
{
    internal class DepartamentTreeService : BaseService<Departament>, IDepartamentTreeService
    {
        private readonly ITreeRepository _treeRepository;

        public DepartamentTreeService(ITreeRepository repository, IResponseFactory<Departament> responceFactory) : base(responceFactory)
        {
            _treeRepository = repository;
        }

        public async Task<ICollectionDataResponse<Departament>> GetFullDepartamentsTree()
        {
            try
            {
                Items = await _treeRepository.GetDepartamentsRelations().ConfigureAwait(false);

                if (Items is null)
                {
                    return _responseFactory.CreateResponce(Resources.AddingErrorMessage, StatusCode.Fail, Items);
                }
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
