using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class GroupUpdateWindowService : BaseWindowService<GroupUpdateWindow>, IGroupUpdateWindowService
    {
        public GroupUpdateWindowService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
