using UniversityTool.Domain.Models;
using UniversityTool.Domain.Response;

namespace UniversityTool.Domain.Services.DataServices
{
    public interface IDepartamentTreeService
    {
        public Task<ICollectionDataResponse<Departament>> GetFullDepartamentsTree();
    }
}
