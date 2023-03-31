using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.ViewModels.Base;
using System;
using UniversityTool.ViewModels.ControlsViewModels;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using System.Windows;

namespace UniversityTool.ViewModels.UpdatingViewModels
{
    internal class GroupUpdateViewModel : BaseGroupViewModel<IGroupUpdateWindowService>
    {
        #region --Fields--

        private readonly TreeViewViewModel _tree;

        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public GroupUpdateViewModel()
        {
            WindowTitle = "Group Update";
        }

        public GroupUpdateViewModel(
            IMessageBusService messageBus,
            IGroupUpdateWindowService windowService,
            IGroupService groupService,
            IDepartamentService departamentService,
            TreeViewViewModel tree) : base(messageBus, windowService, groupService, departamentService)
        {
            _tree = tree;
            GroupName = _tree.SelectedGroup.Title;
            WindowTitle = "Group Update";
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object action)
        {
            var response = await _groupService.Update(new Group
            {
                Id = _tree.SelectedGroup.Id,
                DepartamentId = _tree.SelectedGroup.DepartamentId,
                Title = GroupName,
            }).ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new GroupMessage(response.Data, UIOperationTypeCode.Update));
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
