using System.Collections.Generic;
using UniversityTool.Domain.Models;
using UniversityTool.ViewModels.Base;
using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using System.Threading.Tasks;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using System.Windows;
using System.Linq;

namespace UniversityTool.ViewModels.AddingViemModels
{
    internal class StudentAddViewModel : BaseStudentViewModel<IStudentAddWindowService>
    {
        #region --Fields--

        private IEnumerable<Group> _groups;

        #endregion

        #region --Properties--

        public IEnumerable<Group> Groups
        {
            get => _groups;
            set => Set(ref _groups, value);
        }

        #endregion

        #region --Constructors--

        public StudentAddViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Constructor only for designTime");
            WindowTitle = "Student Add";
        }

        public StudentAddViewModel(IStudentService studentService,
            IGroupService groupService,
            IMessageBusService messageBus,
            IStudentAddWindowService studentAddWindowService) : base(messageBus, studentAddWindowService, groupService, studentService)
        {
            _ = InitializeGroupsAsync();
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object action)
        {
            var response = await _studentService.Add(new Student
            {
                GroupId = SelectedGroup.Id,
                Name = StudentName,
                SecondName = StudentSurname,
                ThirdName = StudentThirdName,
            }).ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new StudentMessage(response.Data, UIOperationTypeCode.Add));
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _windowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                        });
                        break;
                    }
                case OperationResultStatusCode.Fail:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _windowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        });
                        break;
                    }
            }
        }

        #endregion

        #region --Methods--

        private async Task InitializeGroupsAsync()
        {
            var response = await _groupService.GetAll().ConfigureAwait(false);
            if (response.StatusCode == OperationResultStatusCode.Success)
            {
                _ = ProcessInMainThreadAsync(() => Groups = response.Data);
            }
        }

        #endregion
    }
}
