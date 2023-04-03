using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.ViewModels.Base;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Codes;
using System;

namespace UniversityTool.ViewModels.AddingViemModels
{
    internal class GroupAddViewModel : BaseGroupViewModel<IGroupAddWindowService>
    {
        #region --Fields--

        private IEnumerable<Departament> _departaments;

        #endregion

        #region --Properties--

        public IEnumerable<Departament> Departaments
        {
            get => _departaments;
            set => Set(ref _departaments, value);
        }

        #endregion

        #region --Constructors--

        public GroupAddViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
            WindowTitle = "Group Window";
        }

        public GroupAddViewModel(
            IDepartamentService departamentService,
            IGroupService groupService,
            IGroupAddWindowService groupAddWindowService,
            IMessageBusService messageBus) : base(messageBus, groupAddWindowService, groupService, departamentService)
        {
            WindowTitle = "Group Window";
            _ = InitializeDepartamentsAsync();
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object a)
        {
            var response = await _groupService
                .Add(new Group
                {
                    DepartamentId = SelectedDepartament.Id,
                    Title = GroupName
                })
                .ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new GroupMessage(response.Data, UIOperationTypeCode.Add));
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

        private async Task InitializeDepartamentsAsync()
        {
            var response = await Task.Run(_departamentService.GetAll).ConfigureAwait(false);

            if (response.StatusCode == OperationResultStatusCode.Success)
            {
                _ = ProcessInMainThreadAsync(() => Departaments = response.Data);
            }
        }

        #endregion
    }
}
