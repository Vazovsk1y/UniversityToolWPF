using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Responses;
using Microsoft.Extensions.Logging;

namespace UniversityTool.DataBase.Services
{
    internal class DepartamentService : BaseService<Departament>, IDepartamentService
    {
        public DepartamentService(IBaseRepository<Departament> repository, IResponseFactory<Departament> responceFactory, ILogger<DepartamentService> logger) 
            : base(repository, responceFactory, logger)
        {
        }
    }
}
