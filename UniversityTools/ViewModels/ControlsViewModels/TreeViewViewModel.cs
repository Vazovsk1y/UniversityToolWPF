using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models;
using UniversityTool.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Codes;

namespace UniversityTool.ViewModels.ControlsViewModels
{
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
        /// <summary>
        /// Constructor for designer, Debuging instrument.
        /// </summary>
        public TreeViewViewModel()
        {
            if (App.IsDesignMode)
            {
                var students = Enumerable.Range(1, 6).Select(s => new Student { Name = $"Kui {s}" });
                var groups = Enumerable.Range(1, 5).Select(g => new Group { Title = $"Giu {g}", Students = students.ToList() });
                var departaments = Enumerable.Range(1, 10).Select(d => new Departament { Title = $"Siu {d}", Groups = groups.ToList() });
                FullTree = new ObservableCollection<Departament>(departaments);
            }
            else
            {
                throw new InvalidOperationException("This constructor is only for design time");
            }
        }

        public TreeViewViewModel(IDepartamentTreeService treeService, IMessageBusService messageBusService)
        {
            _messageBus = messageBusService;
            _departamentTreeService = treeService;
            _subscriptions.Add(_messageBus.RegisterHandler<DepartamentMessage>(OnReceiveMessage));
            _subscriptions.Add(_messageBus.RegisterHandler<GroupMessage>(OnReceiveMessage));
            _subscriptions.Add(_messageBus.RegisterHandler<StudentMessage>(OnReceiveMessage));
            _ = InitializeFullTreeAsync();
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

        private async Task InitializeFullTreeAsync() => await Task.Run(async () =>
        {
            var response = await _departamentTreeService.GetFullDepartamentsTree().ConfigureAwait(false);
            if (response.StatusCode == OperationResultStatusCode.Success)
            {
                _ = ProcessInMainThreadAsync(() => FullTree = new ObservableCollection<Departament>(response.Data));
            }
        });

        private void OnReceiveMessage(DepartamentMessage message)
        {
            switch (message.OperationType)
            {
                case UIOperationTypeCode.Add:
                    {
                        _ = ProcessInMainThreadAsync(() => FullTree.Add(message.Departament));
                        break;
                    }
                case UIOperationTypeCode.Delete:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            var index = FullTree.IndexOf(SelectedDepartament);
                            if (index != -1)
                            {
                                FullTree.RemoveAt(index);
                                SelectedDepartament = null;
                            }
                        });
                    }
                    break;
                case UIOperationTypeCode.Update:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            var index = FullTree.IndexOf(SelectedDepartament);
                            if (index != -1)
                            {
                                message.Departament.Groups = SelectedDepartament.Groups;

                                FullTree[index] = message.Departament;
                                SelectedDepartament = message.Departament;
                            }
                        });
                        break;
                    }
                case UIOperationTypeCode.Move:
                    break;
            }
        }

        private void OnReceiveMessage(GroupMessage message)
        {
            switch (message.OperationType)
            {
                case UIOperationTypeCode.Add:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            var departament = FullTree.FirstOrDefault(d => d.Id == message.Group.DepartamentId);
                            departament?.Groups.Add(message.Group);
                        });
                        break;
                    }
                case UIOperationTypeCode.Delete:
                    break;
                case UIOperationTypeCode.Update:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            var departament = FullTree.FirstOrDefault(d => d.Id == SelectedGroup.DepartamentId);

                            if (departament is not null)
                            {
                                message.Group.Students = SelectedGroup.Students;

                                var index = departament.Groups.IndexOf(SelectedGroup);
                                departament.Groups[index] = message.Group;
                                SelectedGroup = message.Group;
                            }
                        });
                    }
                    break;
                case UIOperationTypeCode.Move:
                    break;
            }
        }

        private void OnReceiveMessage(StudentMessage message)
        {
            switch (message.OperationType)
            {
                case UIOperationTypeCode.Add:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            var group = FullTree.SelectMany(d => d.Groups).FirstOrDefault(g => g.Id == message.Student.GroupId);
                            group?.Students.Add(message.Student);
                        });
                    }
                    break;
                case UIOperationTypeCode.Delete:
                    break;
                case UIOperationTypeCode.Update:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            var group = FullTree.SelectMany(d => d.Groups).FirstOrDefault(g => g.Id == message.Student.GroupId);
                            
                            if (group is not null)
                            {
                                int index = group.Students.IndexOf(SelectedStudent);
                                if (index is not -1)
                                {
                                    group.Students[index] = message.Student;
                                    SelectedStudent = message.Student;
                                }
                            }
                        });
                    }
                    break;
                case UIOperationTypeCode.Move:
                    break;
            }
        }

        #endregion
    }
}
