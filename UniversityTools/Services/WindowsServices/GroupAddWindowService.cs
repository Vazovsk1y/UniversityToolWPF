using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class GroupAddWindowService : BaseWindowService<GroupAddWindow>, IGroupAddWindowService
    {
        public GroupAddWindowService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
