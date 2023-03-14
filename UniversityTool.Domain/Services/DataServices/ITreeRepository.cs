using UniversityTool.Domain.Models;

namespace UniversityTool.Domain.Services.DataServices
{
    public interface ITreeRepository
    {
        Task<IEnumerable<Departament>> GetFullTree();
    }
}
