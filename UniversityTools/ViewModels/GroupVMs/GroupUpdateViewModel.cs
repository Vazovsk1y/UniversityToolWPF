using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using System;
using UniversityTool.ViewModels.ControlsVMs;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using System.Windows;
using UniversityTool.ViewModels.GroupVMs.Base;

namespace UniversityTool.ViewModels.GroupVMs
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
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
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
            string updatedTitle = GroupName;
            var response = await _groupService.Update(new Group
            {
                Title = updatedTitle,
                Id = _tree.SelectedGroup.Id,
                DepartamentId = _tree.SelectedGroup.DepartamentId,
                Students = _tree.SelectedGroup.Students,
                DateAdded = _tree.SelectedGroup.DateAdded,
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
