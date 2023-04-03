using System;
using System.Windows;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;

namespace UniversityTool.ViewModels.DeletingVIewModels
{
    internal class GroupDeleteViewModel : BaseGroupViewModel<IGroupDeleteWindowService>
    {
        #region --Fields--

        private readonly TreeViewViewModel _tree;

        #endregion

        #region --Properties--

        public string Message => $"Are you sure that you want to delete {_tree.SelectedGroup.Title}";

        #endregion

        #region --Constructors--

        public GroupDeleteViewModel()
        {
            WindowTitle = "Group Delete";
        }

        public GroupDeleteViewModel(IMessageBusService messageBus, 
            IGroupDeleteWindowService windowService, 
            IGroupService groupService, 
            IDepartamentService departamentService,
            TreeViewViewModel tree) : base(messageBus, windowService, groupService, departamentService)
        {
            _tree = tree;
            WindowTitle = "Group Delete";
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object action)
        {
            var response = await _groupService.Delete(_tree.SelectedGroup).ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new GroupMessage(response.Data, UIOperationTypeCode.Delete));
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
