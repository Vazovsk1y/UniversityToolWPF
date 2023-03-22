using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Services.WindowsServices.Implementaions.Base;
using UniversityTool.Views.Windows;

namespace UniversityTool.Services.WindowsServices.Implementaions
{
    internal class StudentAddWindowService : BaseWindowService<StudentAddWindow>, IStudentAddWindowService
    {
        public StudentAddWindowService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
