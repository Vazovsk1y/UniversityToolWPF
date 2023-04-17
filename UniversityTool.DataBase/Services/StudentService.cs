using Microsoft.Extensions.Logging;
using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Responses;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.DataBase.Services
{
    internal class StudentService : BaseService<Student>, IStudentService
    {
        public StudentService(IBaseRepository<Student> repository, IResponseFactory<Student> responceFactory, ILogger<StudentService> logger) 
            : base(repository, responceFactory, logger)
        {
        }
    }
}
