using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class GroupDeleteWindowService : BaseWindowService<GroupDeleteWindow>, IGroupDeleteWindowService
    {
        public GroupDeleteWindowService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
