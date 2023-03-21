using UniversityTool.Domain.Models;

namespace UniversityTool.Domain.Repositories
{
    public interface ITreeRepository
    {
        Task<IEnumerable<Departament>> GetDepartamentsRelations();
    }
}
