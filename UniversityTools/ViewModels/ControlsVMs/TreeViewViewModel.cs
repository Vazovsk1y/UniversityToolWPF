using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models;
using UniversityTool.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Codes;
using UniversityTool.Infastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace UniversityTool.ViewModels.ControlsVMs
{
    /// <summary>
    /// Singleton class that stores the state of the UI tree.
    /// </summary>
    internal class TreeViewViewModel : BaseViewModel, IDisposable
    {
        #region --Fields--

        private Student _selectedStudent;
        private Group _selectedGroup;
        private Departament _selectedDepartament;
        private ObservableCollection<Departament> _fullTree;
        private readonly IDepartamentTreeService _departamentTreeService;
        private readonly IMessageBusService _messageBus;
        private readonly ICollection<IDisposable> _subscriptions = new List<IDisposable>();

        #endregion

        #region --Properties--

        public ObservableCollection<Departament> FullTree
        {
            get => _fullTree;
            private set => Set(ref _fullTree, value);
        }

        public Group SelectedGroup
        {
            get => _selectedGroup;
            private set => Set(ref _selectedGroup, value);
        }

        public Student SelectedStudent
        {
            get => _selectedStudent;
            private set => Set(ref _selectedStudent, value);
        }

        public Departament SelectedDepartament
        {
            get => _selectedDepartament;
            private set => Set(ref _selectedDepartament, value);
        }

        #endregion

        #region --Constructors--

        public TreeViewViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
        }

        public TreeViewViewModel(IDepartamentTreeService treeService, IMessageBusService messageBusService)
        {
            _messageBus = messageBusService;
            _departamentTreeService = treeService;
            _subscriptions.Add(_messageBus.RegisterHandler<DepartamentMessage>(OnReceiveMessage));
            _subscriptions.Add(_messageBus.RegisterHandler<GroupMessage>(OnReceiveMessage));
            _subscriptions.Add(_messageBus.RegisterHandler<StudentMessage>(OnReceiveMessage));
            InitializeFullTree();
        }

        #endregion

        #region --Commands--

        public ICommand TreeViewItemSelectionChangedCommand => new RelayCommand(OnTreeViewItemSelectionChanged);

        private void OnTreeViewItemSelectionChanged(object selectedItem)
        {
            // depends on selected item type
            switch (selectedItem)
            {
                case Student student:
                    {
                        SelectedDepartament = null;
                        SelectedGroup = null;
                        SelectedStudent = student;
                    }
                    break;
                case Group group:
                    {
                        SelectedStudent = null;
                        SelectedDepartament = null;
                        SelectedGroup = group;
                    }
                    break;
                case Departament departament:
                    {
                        SelectedGroup = null;
                        SelectedStudent = null;
                        SelectedDepartament = departament;
                    }
                    break;
            }
        }

        #endregion

        #region --Methods--

        public void Dispose()
        {
            foreach(IDisposable subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private Task InitializeFullTree() => Task.Run(async () =>
        {
            var response = await _departamentTreeService.GetFullDepartamentsTree().ConfigureAwait(false);
            if (response.StatusCode == OperationResultStatusCode.Success)
            {
                _ = ProcessInMainThreadAsync(() => FullTree = new ObservableCollection<Departament>(response.Data));
            }
        });

        private void OnReceiveMessage(DepartamentMessage message) => MessageHandler(message);

        private void OnReceiveMessage(GroupMessage message) => MessageHandler(message);

        private void OnReceiveMessage(StudentMessage message) => MessageHandler(message);

        private Task MessageHandler(DepartamentMessage message) => message.OperationType switch
        {
            UIOperationTypeCode.Add => ProcessInMainThreadAsync(() => FullTree.AddDepapartament(message.Departament)),
            UIOperationTypeCode.Delete => ProcessInMainThreadAsync(() => 
            {
                FullTree.DeleteDepartament(SelectedDepartament);
                if (FullTree.Count is 0)
                {
                    SelectedDepartament = null;
                }
            }),
            UIOperationTypeCode.Update => ProcessInMainThreadAsync(() => 
            { 
                FullTree.UpdateDepartament(SelectedDepartament, message.Departament); 
                SelectedDepartament = message.Departament; 
            }),
            UIOperationTypeCode.Move => Task.CompletedTask,
            _ => Task.CompletedTask
        };

        private Task MessageHandler(GroupMessage message) => message.OperationType switch
        {
            UIOperationTypeCode.Add => _ = ProcessInMainThreadAsync(() => FullTree.AddGroup(message.Group)),
            UIOperationTypeCode.Delete => _ = ProcessInMainThreadAsync(() => 
            { 
                FullTree.DeleteGroup(SelectedGroup);
            }),
            UIOperationTypeCode.Update => _ = ProcessInMainThreadAsync(() =>
            {
                FullTree.UpdateGroup(SelectedGroup, message.Group);
                SelectedGroup = message.Group;
            }),
            UIOperationTypeCode.Move => Task.CompletedTask,
            _ => Task.CompletedTask,
        };

        private Task MessageHandler(StudentMessage message) => message.OperationType switch
        {
            UIOperationTypeCode.Add => _ = ProcessInMainThreadAsync(() => FullTree.AddStudent(message.Student)),
            UIOperationTypeCode.Delete => Task.CompletedTask,
            UIOperationTypeCode.Update => _ = ProcessInMainThreadAsync(() =>
            {
                FullTree.UpdateStudent(SelectedStudent, message.Student);
                SelectedStudent = message.Student;
            }),
            UIOperationTypeCode.Move => Task.CompletedTask,
            _ => Task.CompletedTask,
        };

        #endregion
    }
}
