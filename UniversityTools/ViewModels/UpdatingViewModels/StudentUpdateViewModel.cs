﻿using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.ViewModels.Base;
using System;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.ViewModels.ControlsViewModels;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using System.Windows;
using System.Linq;

namespace UniversityTool.ViewModels.UpdatingViewModels
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
                Name = StudentName,
                SecondName = StudentSurname,
                ThirdName = StudentThirdName
            });

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