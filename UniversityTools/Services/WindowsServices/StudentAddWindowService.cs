using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices
{
    internal class StudentAddWindowService : BaseWindowService<StudentAddWindow>, IStudentAddWindowService
    {
        public StudentAddWindowService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
