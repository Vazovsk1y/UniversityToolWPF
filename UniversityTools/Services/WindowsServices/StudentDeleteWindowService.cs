using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class StudentDeleteWindowService : BaseWindowService<StudentDeleteWindow>, IStudentDeleteWindowService
    {
        public StudentDeleteWindowService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
