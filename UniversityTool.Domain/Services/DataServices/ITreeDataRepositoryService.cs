using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.DataServices.Base;

namespace UniversityTool.Domain.Services.DataServices
{
    public interface ITreeDataRepositoryService : IBaseDataRepositoryService<Departament>
    {
        Task<IEnumerable<Departament>> GetFullTree();
    }
}
