using System;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Models.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;
using UniversityTool.Domain.Services.WindowsServices;

namespace UniversityTool.ViewModels
{
    // IDisposable if i want to take messages(data) from other windows
    internal class MainWindowViewModel : BaseViewModel, IDisposable
    {
        #region --Fields--

        private readonly IDepartamentAddWindowService _departamentAddWindow;
        private readonly IGroupAddWindowService _groupAddWindow;
        private readonly IMessageBusService _messageBus;
        private readonly IDisposable _subscription;

        #endregion

        #region --Properties--                 

        public TreeViewViewModel TreeViewViewModel { get; }

        #endregion

        #region --Constructors--

        public MainWindowViewModel() 
        {
            TreeViewViewModel = new TreeViewViewModel();
            AddDepartamentCommand = new RelayCommand(OnAddingDepartament, OnCanAddDepartament);
            AddGroupCommand = new RelayCommand(OnAddingGroup, OnCanAddGroup);
        }

        public MainWindowViewModel(IDepartamentAddWindowService service, IMessageBusService messageBus, 
            IGroupAddWindowService groupAddWindowService) : this()
        {
            _groupAddWindow = groupAddWindowService;
            _departamentAddWindow = service;
            _messageBus = messageBus;
            _subscription = _messageBus.RegisterHandler<DepartamentTitleMessage>(OnReceiveMessage);
        }

        #endregion

        #region --Commands--

        public ICommand AddDepartamentCommand { get; }                          // initialize in class constructor

        private bool OnCanAddDepartament(object arg) => true;

        private void OnAddingDepartament(object obj) => _departamentAddWindow.OpenWindow();

        public ICommand AddGroupCommand { get; }                          // initialize in class constructor

        private bool OnCanAddGroup(object arg) => true;

        private void OnAddingGroup(object obj) => _groupAddWindow.OpenWindow();

        #endregion

        #region --Methods--

        public void Dispose() => _subscription?.Dispose();

        private void OnReceiveMessage(DepartamentTitleMessage message)
        {
            if (message is null || message.DepartamentTitle is null) return;

            if (message.DepartamentTitle.Length != 0)
            {
                TreeViewViewModel.Departaments.Add(new Departament { Title = message.DepartamentTitle });
            }
        }

        #endregion
    }
}
