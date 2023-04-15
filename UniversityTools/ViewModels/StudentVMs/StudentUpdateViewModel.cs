using UniversityTool.Domain.Services.WindowsServices;
using System;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.ViewModels.ControlsVMs;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using System.Windows;
using System.Linq;
using UniversityTool.ViewModels.StudentVMs.Base;

namespace UniversityTool.ViewModels.StudentVMs
{
    internal class StudentUpdateViewModel : BaseStudentViewModel<IStudentUpdateWindowService>
    {
        #region --Fields--

        private readonly TreeViewViewModel _tree;

        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public StudentUpdateViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
            WindowTitle = "Student Update";
        }

        public StudentUpdateViewModel(IMessageBusService messageBus,
            IStudentUpdateWindowService windowService,
            IGroupService groupService,
            IStudentService studentService,
            TreeViewViewModel tree) : base(messageBus, windowService, groupService, studentService)
        {
            _tree = tree;
            StudentName = _tree.SelectedStudent.Name;
            StudentSurname = _tree.SelectedStudent.SecondName;
            StudentThirdName = _tree.SelectedStudent.ThirdName;
            WindowTitle = "Student Update";
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object action)
        {
            var response = await _studentService.Update(new Student
            {
                Id = _tree.SelectedStudent.Id,
                GroupId = _tree.SelectedStudent.GroupId,
                DateAdded = _tree.SelectedStudent.DateAdded,
                Name = StudentName,
                SecondName = StudentSurname,
                ThirdName = StudentThirdName,
            }).ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new StudentMessage(response.Data, UIOperationTypeCode.Update));
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _windowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                        });
                    }
                    break;
                case OperationResultStatusCode.Fail:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _windowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        });
                    }
                    break;
            }
        }

        #endregion

        #region --Methods--



        #endregion
    }
}
