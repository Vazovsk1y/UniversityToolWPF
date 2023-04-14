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
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;

namespace UniversityTool.ViewModels.ControlsVMs
{
    /// <summary>
    /// Singleton class that stores the state of the UI tree.
    /// </summary>
    internal class TreeViewViewModel : BaseViewModel, IDisposable
    {
        #region --Fields--

        private string _departamentFilterText;
        private object _selectedItem;
        private bool _isGroupSelected;
        private bool _isStudentSelected;
        private bool _isAnyItemSelected;
        private Student _selectedStudent;
        private Group _selectedGroup;
        private Departament _selectedDepartament;
        private ObservableCollection<Departament> _fullTree;
        private readonly IDepartamentTreeService _departamentTreeService;
        private readonly IMessageBusService _messageBus;
        private readonly ICollection<IDisposable> _subscriptions = new List<IDisposable>();
        private readonly CollectionViewSource _filtredFullTree = new();

        #endregion

        #region --Properties--

        public ICollectionView FiltredFullTreeView => _filtredFullTree?.View;

        public string DepartamentFilterText
        {
            get => _departamentFilterText;
            set
            {
                if (Set(ref _departamentFilterText, value))
                {
                    FiltredFullTreeView.Refresh();
                }
            }
        }

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                IsAnyItemSelected = value is not null;

                switch (value)
                {
                    case Departament departament:
                        {
                            SelectedGroup = null;
                            SelectedStudent = null;
                            SelectedDepartament = departament;
                        }
                        break;
                    case Group group:
                        {
                            SelectedDepartament = null;
                            SelectedStudent = null;
                            SelectedGroup = group;
                        }
                        break;
                    case Student student:
                        {
                            SelectedDepartament = null;
                            SelectedGroup = null;
                            SelectedStudent = student;
                        }
                        break;
                    case null:
                        {
                            SelectedDepartament = null;
                            SelectedGroup = null;
                            SelectedStudent = null;
                        }
                        break;
                }

                Set(ref _selectedItem, value);
            }
        }

        public bool IsGroupSelected
        {
            get => _isGroupSelected;
            set => Set(ref _isGroupSelected, value);
        }

        public bool IsStudentSelected
        {
            get => _isStudentSelected;
            private set => Set(ref _isStudentSelected, value);
        }

        public bool IsAnyItemSelected
        {
            get => _isAnyItemSelected;
            private set => Set(ref _isAnyItemSelected, value);
        }

        public ObservableCollection<Departament> FullTree
        {
            get => _fullTree;
            private set
            {
                if (Set(ref _fullTree, value))
                {
                    _filtredFullTree.Source = value;
                    OnPropertyChanged(nameof(FiltredFullTreeView));
                }
            }
        }

        public Group SelectedGroup
        {
            get => _selectedGroup;
            private set
            {
                IsGroupSelected = value is not null;
                Set(ref _selectedGroup, value);
            }
        }

        public Student SelectedStudent
        {
            get => _selectedStudent;
            private set
            {
                IsStudentSelected = value is not null;
                Set(ref _selectedStudent, value);
            }
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

            var groups = Enumerable.Range(0, 5).Select(g => new Group { Title = $"Mama Mia{g}" }).ToList();
            var departaments = Enumerable.Range(1, 10).Select(d => new Departament { Title = $"Siu {d}", Groups = groups }).ToList();
            FullTree = new ObservableCollection<Departament>(departaments);
        }

        public TreeViewViewModel(
            IDepartamentTreeService departamentTreeService,
            IMessageBusService messageBusService)
        {
            _messageBus = messageBusService;
            _departamentTreeService = departamentTreeService;
            _subscriptions.Add(_messageBus.RegisterHandler<DepartamentMessage>(OnReceiveMessage));
            _subscriptions.Add(_messageBus.RegisterHandler<GroupMessage>(OnReceiveMessage));
            _subscriptions.Add(_messageBus.RegisterHandler<StudentMessage>(OnReceiveMessage));
            _filtredFullTree.Filter += OnDepartamentTreeViewFilter;
            _filtredFullTree.SortDescriptions.Add(new SortDescription(nameof(Departament.Title), ListSortDirection.Ascending));
            InitializeFullTree();
        }

        #endregion

        #region --Commands--

        public ICommand TreeViewItemSelectionChangedCommand => new RelayCommand((selectedItem) =>
        {
            _messageBus.Send(new TabsPanelMessage(selectedItem, UIOperationTypeCode.Add));
            SelectedItem = selectedItem;
        });

        #endregion

        #region --Methods--

        public void Dispose()
        {
            foreach(IDisposable subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private void OnDepartamentTreeViewFilter(object sender, FilterEventArgs e)
        {
            var filterText = DepartamentFilterText;
            if (string.IsNullOrEmpty(filterText)) 
                return;

            if (e.Item is Departament departament && departament.Title is not null) 
            {
                if (departament.Title.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
                e.Accepted = false;
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
                _messageBus.Send(new TabsPanelMessage(SelectedDepartament, UIOperationTypeCode.Delete));
                FullTree.DeleteDepartament(SelectedDepartament);
                if (FullTree.Count is 0)
                {
                    SelectedItem = null;
                }
            }),
            UIOperationTypeCode.Update => ProcessInMainThreadAsync(() => 
            { 
                FullTree.UpdateDepartament(SelectedDepartament, message.Departament);
                SelectedItem = message.Departament; 
            }),
            UIOperationTypeCode.Move => Task.CompletedTask,
            _ => Task.CompletedTask
        };

        private Task MessageHandler(GroupMessage message) => message.OperationType switch
        {
            UIOperationTypeCode.Add => _ = ProcessInMainThreadAsync(() => FullTree.AddGroup(message.Group)),
            UIOperationTypeCode.Delete => _ = ProcessInMainThreadAsync(() => 
            {
                _messageBus.Send(new TabsPanelMessage(SelectedGroup, UIOperationTypeCode.Delete));
                FullTree.DeleteGroup(SelectedGroup); 
            }),
            UIOperationTypeCode.Update => _ = ProcessInMainThreadAsync(() =>
            {
                FullTree.UpdateGroup(SelectedGroup, message.Group);
                SelectedItem = message.Group;
            }),
            UIOperationTypeCode.Move => Task.CompletedTask,
            _ => Task.CompletedTask,
        };

        private Task MessageHandler(StudentMessage message) => message.OperationType switch
        {
            UIOperationTypeCode.Add => _ = ProcessInMainThreadAsync(() => FullTree.AddStudent(message.Student)),
            UIOperationTypeCode.Delete => _ = ProcessInMainThreadAsync(() =>
            {
                _messageBus.Send(new TabsPanelMessage(SelectedStudent, UIOperationTypeCode.Delete));
                FullTree.DeleteStudent(SelectedStudent);
            }),
            UIOperationTypeCode.Update => _ = ProcessInMainThreadAsync(() =>
            {
                FullTree.UpdateStudent(SelectedStudent, message.Student);
                SelectedItem = message.Student;
            }),
            UIOperationTypeCode.Move => Task.CompletedTask,
            _ => Task.CompletedTask,
        };

        #endregion
    }
}
