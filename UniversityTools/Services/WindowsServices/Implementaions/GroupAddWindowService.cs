using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Implementaions.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices.Implementaions
{
    internal class GroupAddWindowService : BaseWindowService<GroupAddWindow>, IGroupAddWindowService
    {
        public GroupAddWindowService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
