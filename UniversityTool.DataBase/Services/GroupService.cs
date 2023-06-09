﻿using Microsoft.Extensions.Logging;
using UniversityTool.DataBase.Services.Base;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Responses;
using UniversityTool.Domain.Services.DataServices;

namespace UniversityTool.DataBase.Services
{
    internal class GroupService : BaseService<Group>, IGroupService
    {
        public GroupService(IBaseRepository<Group> repository, IResponseFactory<Group> responceFactory, ILogger<GroupService> logger) 
            : base(repository, responceFactory, logger)
        {
        }
    }
}
