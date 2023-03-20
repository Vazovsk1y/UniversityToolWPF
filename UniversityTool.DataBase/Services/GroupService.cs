using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Response;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.DataBase.Services
{
    internal class GroupService : BaseService<Group>, IGroupService
    {
        public GroupService(IBaseRepository<Group> repository, IResponseFactory<Group> responceFactory) : base(repository, responceFactory)
        {
        }
    }
}
