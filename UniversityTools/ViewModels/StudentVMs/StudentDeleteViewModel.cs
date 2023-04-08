using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.ViewModels.ControlsVMs;
using UniversityTool.ViewModels.StudentVMs.Base;

namespace UniversityTool.ViewModels.StudentVMs
{
    internal class StudentDeleteViewModel : BaseStudentViewModel<IStudentDeleteWindowService>
    {
        #region --Fields--

        private readonly TreeViewViewModel _tree;

        #endregion

        #region --Properties--

        public string Message => $"Are you sure that you want to delete {_tree.SelectedStudent.FullName}";

        #endregion

        #region --Constructors--

        public StudentDeleteViewModel() 
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
            WindowTitle = "Student Delete";
        }

        public StudentDeleteViewModel(
            IMessageBusService messageBus, 
            IStudentDeleteWindowService windowService, 
            IGroupService groupService, 
            IStudentService studentService,
            TreeViewViewModel tree) : base(messageBus, windowService, groupService, studentService)
        {
            _tree = tree;
            WindowTitle = "Student Delete";
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object action)
        {
            var response = await _studentService.Delete(_tree.SelectedStudent).ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new StudentMessage(response.Data, UIOperationTypeCode.Delete));
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

        protected override bool OnCanAccept(object p) => true;
        
        #endregion

        #region --Methods--



        #endregion
    }
}
