using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class DepartamentDeleteWindowService : BaseWindowService<DepartamentDeleteWindow>, IDepartamentDeleteWindowService
    {
        public DepartamentDeleteWindowService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
