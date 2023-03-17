using UniversityTool.Domain.Models;
using UniversityTool.Domain.Response;

namespace UniversityTool.Domain.Services.DataServices
{
    public interface IDepartamentService
    {
        Task<IBaseResponse<Departament>> AddDepartament(string Title);
    }
}
