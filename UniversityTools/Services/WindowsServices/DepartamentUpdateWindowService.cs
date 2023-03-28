using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class DepartamentUpdateWindowService : BaseWindowService<DepartamentUpdateWindow>, IDepartamentUpdateWindowService
    {
        public DepartamentUpdateWindowService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
