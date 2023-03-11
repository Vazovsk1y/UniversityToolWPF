using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Models.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Infastructure.Commands;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels
{
    internal class GroupAddViewModel : BaseViewModel
    {
        #region --Fields--

        private string _groupName;
        private Departament _selectedDepartament;
        private ObservableCollection<Departament> _departaments;

        #region --Services--

        private readonly IBaseDataRepositoryService<Group> _dataGroupService;
        private readonly IBaseDataRepositoryService<Departament> _dataDepartamentService;
        private readonly IGroupAddWindowService _groupAddWindowService;
        private readonly IMessageBusService _messageBus;

        #endregion

        #endregion

        #region --Properties--

        public ObservableCollection<Departament> Departaments
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
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
            AcceptCommand = new RelayCommand(OnAccepting, OnCanAccept);
        }

        public GroupAddViewModel(IBaseDataRepositoryService<Departament> dataService, IGroupAddWindowService groupAddWindowService,
            IMessageBusService messageBusService, IBaseDataRepositoryService<Group> dataGroupService) : this()
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

        //private void InitializeDepartamentsAsync()
        //{
        //    Task.Run(async () =>
        //    {
        //        IEnumerable<Departament> departaments = await _dataService.GetAll().ConfigureAwait(false);
        //        await Application.Current.Dispatcher.InvokeAsync(() =>
        //        {
        //            Departaments = new ObservableCollection<Departament>(departaments);
        //        });
        //    });
        //}

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
