using UniversityTool.Domain.Models;

namespace UniversityTool.Domain.Repositories
{
    public interface IDepartamentTreeRepository
    {
        Task<IEnumerable<Departament>> GetDepartamentsRelations();
    }
}
