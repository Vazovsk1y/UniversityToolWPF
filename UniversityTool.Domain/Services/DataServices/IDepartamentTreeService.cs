using UniversityTool.Domain.Models;
using UniversityTool.Domain.Responses;

namespace UniversityTool.Domain.Services.DataServices
{
    public interface IDepartamentTreeService
    {
        public Task<ICollectionDataResponse<Departament>> GetFullDepartamentsTree();
    }
}
