using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Infastructure.Commands;
using UniversityTool.ViewModels.Base;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Codes;

namespace UniversityTool.ViewModels
{
    internal class GroupAddViewModel : TitledViewModel
    {
        #region --Fields--

        private string _groupName;
        private Departament _selectedDepartament;
        private IEnumerable<Departament> _departaments;

        #region --Services--

        private readonly IDepartamentService _departamentService;
        private readonly IGroupService _groupService;
        private readonly IGroupAddWindowService _groupAddWindowService;
        private readonly IMessageBusService _messageBus;

        #endregion

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
            WindowTitle = "Group Window";
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
            AcceptCommand = new RelayCommand(OnAcceptingAsync, OnCanAccept);
        }

        public GroupAddViewModel(IDepartamentService departamentService, 
            IGroupService groupService, 
            IGroupAddWindowService groupAddWindowService, 
            IMessageBusService messageBus) : this()
        {
            _departamentService = departamentService;
            _groupService = groupService;
            _groupAddWindowService = groupAddWindowService;
            _messageBus = messageBus;
            _ = InitializeDepartamentsAsync();
        }

        #endregion

        #region --Commands--

        public ICommand CancelCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        private bool OnCanCancel(object p) => true;

        private void OnCanceling(object p) => _groupAddWindowService.CloseWindow();

        private bool OnCanAccept(object p) => true;

        private async void OnAcceptingAsync(object a)
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
                case OperationStatusCode.Success:
                    {
                        await SendMessageAsync(response.Data);
                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            _groupAddWindowService.CloseWindow();
                            MessageBox.Show(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                        });
                        break;
                    }
                case OperationStatusCode.Fail:
                    {
                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            _groupAddWindowService.CloseWindow();
                            MessageBox.Show(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (response.StatusCode == OperationStatusCode.Success)
            {
                _ = ProcessInMainThreadAsync(() => Departaments = response.Data);
            }
        }

        private async Task SendMessageAsync(Group entity) => await Task
            .Run(() => _messageBus
            .Send(new GroupMessage(entity, OperationTypeCode.Add)))
            .ConfigureAwait(false);

        #endregion
    }
}
