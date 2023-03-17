using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Repositories.Base;
using UniversityTool.Domain.Services;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Infastructure.Commands;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels
{
    internal class GroupAddViewModel : TitledViewModel
    {
        #region --Fields--

        private string _groupName;
        private Departament _selectedDepartament;
        private IEnumerable<Departament> _departaments;

        #region --Services--

        private readonly IBaseRepository<Group> _dataGroupService;
        private readonly IBaseRepository<Departament> _dataDepartamentService;
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
            get => _selectedDepartament; 
            set => Set(ref _selectedDepartament, value); 
        }

        #endregion

        #region --Constructors--

        public GroupAddViewModel()
        {
            Title = "Group Window";
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
            AcceptCommand = new RelayCommand(OnAccepting, OnCanAccept);
        }

        public GroupAddViewModel(IBaseRepository<Departament> dataService, IGroupAddWindowService groupAddWindowService,
            IMessageBusService messageBusService, IBaseRepository<Group> dataGroupService) : this()
        {
            _dataGroupService = dataGroupService;
            _messageBus = messageBusService;
            _dataDepartamentService = dataService;
            _groupAddWindowService = groupAddWindowService;
            InitializeDepartamentsAsync();
        }

        #endregion

        #region --Commands--

        public ICommand CancelCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        private bool OnCanCancel(object p) => true;

        private void OnCanceling(object p) => _groupAddWindowService.CloseWindow();

        private bool OnCanAccept(object p) => true;

        private async void OnAccepting(object a)
        {
            var groupEntity = await _dataGroupService
                .Add(new Group 
                { 
                    DepartamentId = SelectedDepartament.Id, 
                    Title = GroupTitle 
                })
                .ConfigureAwait(false);

            await SendMessageAndCloseWindowAsync(groupEntity);
        }

        #endregion

        #region --Methods--

        private async void InitializeDepartamentsAsync()
        {
            IEnumerable<Departament> departaments = await Task.Run(_dataDepartamentService.GetAll).ConfigureAwait(false);
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Departaments = new ObservableCollection<Departament>(departaments);
            });
        }

        private async Task SendMessageAndCloseWindowAsync(Group group)
        {
            await Task.Run(() =>
            {
                _messageBus.Send(new GroupMessage(group));
            }).ConfigureAwait(false);

            await Application.Current.Dispatcher.InvokeAsync(_groupAddWindowService.CloseWindow);
        }

        #endregion
    }
}
