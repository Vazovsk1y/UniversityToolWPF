using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Models;
using UniversityTool.Models.Messages;
using UniversityTool.Services;
using UniversityTool.Services.Implementaions;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;

namespace UniversityTool.ViewModels
{
    // IDisposable if i want to take messages from out
    internal class MainWindowViewModel : BaseViewModel, IDisposable
    {
        #region --Fields--

        private readonly IDepartamentAddService _departamentAdd;
        private readonly IMessageBus _messageBus;
        private readonly IDisposable _subscription;

        #endregion

        #region --Properties--

        public ICommand AddDepartamentCommand { get; }
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
            _subscription = _messageBus.RegisterHandler<DepartamentAddMessage>(OnReceiveMessage);
        }

        #endregion

        #region --Methods--

        private bool OnCanAdd(object arg) => true;

        private void OnAdding(object obj)
        {
            _departamentAdd.OpenWindow();
        }

        public void Dispose() => _subscription?.Dispose();

        private void OnReceiveMessage(DepartamentAddMessage message)
        {
            if (message is null || message.Departament.Title == null) return;

            if (message.Departament.Title.Length != 0)
                TreeViewViewModel.Departaments.Add(new Departament { Title = message.Departament.Title });
        }

        #endregion
    }
}
