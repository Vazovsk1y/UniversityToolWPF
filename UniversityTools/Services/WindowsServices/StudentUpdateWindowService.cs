using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class StudentUpdateWindowService : BaseWindowService<StudentUpdateWindow>, IStudentUpdateWindowService
    {
        public StudentUpdateWindowService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
