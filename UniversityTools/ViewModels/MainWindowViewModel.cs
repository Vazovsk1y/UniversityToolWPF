using System;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Models.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Domain.Services.DataServices;
using System.Windows;

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

        public TreeViewViewModel TreeView { get; }

        #endregion

        #region --Constructors--

        public MainWindowViewModel() 
        {
            AddDepartamentCommand = new RelayCommand(OnAddingDepartament, OnCanAddDepartament);
            AddGroupCommand = new RelayCommand(OnAddingGroup, OnCanAddGroup);
        }

        public MainWindowViewModel(IDepartamentAddWindowService service, IMessageBusService messageBus, 
            IGroupAddWindowService groupAddWindowService, ITreeDataRepositoryService dataRepository) : this()
        {
            _groupAddWindow = groupAddWindowService;
            _departamentAddWindow = service;
            _messageBus = messageBus;
            _subscription = _messageBus.RegisterHandler<DepartamentMessage>(OnReceiveMessage);
            TreeView = new TreeViewViewModel(dataRepository);
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

        private void OnReceiveMessage(DepartamentMessage message)
        {
            if (message is null || message.Departament.Title is null) return;

            if (message.Departament.Title.Length != 0)
            {
                Application.Current.Dispatcher.Invoke(() => {
                    TreeView.Departaments.Add(message.Departament);
                });
            }
        }

        #endregion
    }
}
