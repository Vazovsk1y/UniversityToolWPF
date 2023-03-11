using UniversityTool.Domain.Models;

namespace UniversityTool.Domain.Services.DataServices
{
    public interface ITreeDataRepositoryService
    {
        Task<IEnumerable<Departament>> GetFullTree();
    }
}
