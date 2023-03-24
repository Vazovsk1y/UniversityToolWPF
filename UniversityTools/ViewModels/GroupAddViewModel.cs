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

namespace UniversityTool.ViewModels
{
    internal class GroupAddViewModel : DialogViewModel<IGroupAddWindowService>
    {
        #region --Fields--

        private string _groupName;
        private Departament _selectedDepartament;
        private IEnumerable<Departament> _departaments;
        private readonly IDepartamentService _departamentService;
        private readonly IGroupService _groupService;

        #endregion

        #region --Properties--

        public IEnumerable<Departament> Departaments
        {
            get => _departaments;
            set => Set(ref _departaments, value);                         
        }

        public string GroupTitle 
        {
            get => _groupName; 
            set => Set(ref _groupName, value); 
        }

        public Departament SelectedDepartament 
        { 
            get => _selectedDepartament ?? new(); 
            set => Set(ref _selectedDepartament, value); 
        }

        #endregion

        #region --Constructors--

        public GroupAddViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Standart constructor is only for design time");
            WindowTitle = "Group Window";
        }

        public GroupAddViewModel(IDepartamentService departamentService, 
            IGroupService groupService, 
            IGroupAddWindowService groupAddWindowService, 
            IMessageBusService messageBus) : base(messageBus, groupAddWindowService)
        {
            _departamentService = departamentService;
            _groupService = groupService;
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
                    Title = GroupTitle
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
