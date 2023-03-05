using System;
using UniversityTool.Domain.Services;
using UniversityTool.Services.WindowsServices.Implementaions.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices.Implementaions
{
    internal class DepartamentAddService : BaseWindowService<DepartamentAddWindow>, IDepartamentAddService
    {
        public DepartamentAddService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        protected override DepartamentAddWindow? Window { get; set; }
    }
}
