using System;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Models;
using UniversityTool.Models.Messages;
using UniversityTool.Services;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;

namespace UniversityTool.ViewModels
{
    // IDisposable if i want to take messages(data) from other windows
    internal class MainWindowViewModel : BaseViewModel, IDisposable
    {
        #region --Fields--

        private readonly IDepartamentAddService _departamentAdd;
        private readonly IMessageBus _messageBus;
        private readonly IDisposable _subscription;

        #endregion

        #region --Properties--                 

        public TreeViewViewModel TreeViewViewModel { get; }

        #endregion

        #region --Constructors--

        public MainWindowViewModel() 
        {
            TreeViewViewModel = new TreeViewViewModel();
            AddDepartamentCommand = new RelayCommand(OnAdding, OnCanAdd);
        }

        public MainWindowViewModel(IDepartamentAddService service, IMessageBus messageBus) : this()
        {
            _departamentAdd = service;
            _messageBus = messageBus;
            _subscription = _messageBus.RegisterHandler<DepartamentTitleMessage>(OnReceiveMessage);
        }

        #endregion

        #region --Commands--

        public ICommand AddDepartamentCommand { get; }                          // initialize in class constructor

        private bool OnCanAdd(object arg) => true;

        private void OnAdding(object obj) => _departamentAdd.OpenWindow();
        
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
