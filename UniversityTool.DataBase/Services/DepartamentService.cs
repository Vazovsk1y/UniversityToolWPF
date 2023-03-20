using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Response;

namespace UniversityTool.DataBase.Services
{
    internal class DepartamentService : BaseService<Departament>, IDepartamentService
    {
        public DepartamentService(IBaseRepository<Departament> repository, IResponseFactory<Departament> responceFactory) : base(repository, responceFactory)
        {
        }
    }
}
