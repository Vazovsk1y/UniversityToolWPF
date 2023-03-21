using System;
using UniversityTool.Domain.Services;
using UniversityTool.Services.WindowsServices.Implementaions.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices.Implementaions
{
    internal class DepartamentAddWindowService : BaseWindowService<DepartamentAddWindow>, IDepartamentAddWindowService
    {
        public DepartamentAddWindowService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
