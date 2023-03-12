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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UniversityTool.ViewModels
{
    // IDisposable if i want to take messages(data) from other windows
    internal class MainWindowViewModel : TitledViewModel, IDisposable
    {
        #region --Fields--

        private readonly IDepartamentAddWindowService _departamentAddWindow;
        private readonly IGroupAddWindowService _groupAddWindow;
        private readonly IMessageBusService _messageBus;
        private readonly ICollection<IDisposable> _subscriptions = new List<IDisposable>();

        #endregion

        #region --Properties--                 

        public TreeViewViewModel TreeView { get; }

        #endregion

        #region --Constructors--

        public MainWindowViewModel() 
        {
            Title = "UniversityTool";
            AddDepartamentCommand = new RelayCommand(OnAddingDepartament, OnCanAddDepartament);
            AddGroupCommand = new RelayCommand(OnAddingGroup, OnCanAddGroup);
        }

        public MainWindowViewModel(IDepartamentAddWindowService service, IMessageBusService messageBus, 
            IGroupAddWindowService groupAddWindowService, TreeViewViewModel treeView) : this()
        {
            _groupAddWindow = groupAddWindowService;
            _departamentAddWindow = service;
            _messageBus = messageBus;
            TreeView = treeView;
            _subscriptions.Add(messageBus.RegisterHandler<DepartamentMessage>(OnReceiveMessage));
            _subscriptions.Add(messageBus.RegisterHandler<GroupMessage>(OnReceiveMessage));
        }

        #endregion

        #region --Commands--

        public ICommand AddDepartamentCommand { get; }                          

        private bool OnCanAddDepartament(object arg) => true;

        private void OnAddingDepartament(object obj) => _departamentAddWindow.OpenWindow();

        public ICommand AddGroupCommand { get; }                          

        private bool OnCanAddGroup(object arg) => true;

        private void OnAddingGroup(object obj) => _groupAddWindow.OpenWindow();

        #endregion

        #region --Methods--

        public void Dispose()
        {
            foreach(var subscription in _subscriptions)
                subscription.Dispose();
        }

        private async void OnReceiveMessage(DepartamentMessage message)
        {
            if (message is null || message.Departament.Title is null) return;

            if (message.Departament.Title.Length != 0)
            {
                await Application.Current.Dispatcher.InvokeAsync(() => 
                {
                    TreeView.Departaments.Add(message.Departament);
                });
            }
        }

        private async void OnReceiveMessage(GroupMessage message)
        {
            message.Deconstruct(out Group group);

            if (group is null || group.Title is null)
                return;

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var departament = TreeView.Departaments.FirstOrDefault(d => d.Id == group.DepartamentId);
                departament?.Groups.Add(group);
            });
        }

        #endregion
    }
}
