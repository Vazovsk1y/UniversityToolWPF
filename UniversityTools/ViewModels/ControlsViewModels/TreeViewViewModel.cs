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
        private readonly IDepartamentTreeService _treeService;
        private readonly IMessageBusService _messageBus;
        private readonly List<IDisposable> _subscriptions = new();

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

        public TreeViewViewModel(IDepartamentTreeService dataService, IMessageBusService messageBusService)
        {
            _messageBus = messageBusService;
            _treeService = dataService;
            _subscriptions.Add(_messageBus.RegisterHandler<DepartamentMessage>(OnReceiveMessageAsync));
            _subscriptions.Add(_messageBus.RegisterHandler<GroupMessage>(OnReceiveMessage));
            TreeViewItemSelectionChangedCommand = new RelayCommand(OnTreeViewItemSelectionChanged, OnCanSelectTreeViewItem);
            _ = InitializeFullTreeAsync();
        }

        #endregion

        #region --Commands--

        public ICommand TreeViewItemSelectionChangedCommand { get; private set; }

        private bool OnCanSelectTreeViewItem(object arg) => true;

        private void OnTreeViewItemSelectionChanged(object selectedItem)
        {
            // depends on selected item type
            switch (selectedItem)
            {
                case Student student:
                    SelectedStudent = student;
                    break;
                case Group group:
                    SelectedGroup = group;
                    break;
                case Departament departament:
                    SelectedDepartament = departament;
                    break;
            }
        }

        #endregion

        #region --Methods--

        public void Dispose() => _subscriptions.ForEach(subscription => subscription.Dispose());

        //private async Task InitializeFullTreeAsync()
        //{
        //    var response = await _treeService.GetFullDepartamentsTree().ConfigureAwait(false);
        //    if (response.StatusCode == StatusCode.Success)
        //    {
        //        _ = ProcessInMainThreadAsync(() => FullTree = new ObservableCollection<Departament>(response.Data));
        //    }
        //}

        private async Task InitializeFullTreeAsync() => await Task.Run(async () =>
            {
                var response = await _treeService.GetFullDepartamentsTree().ConfigureAwait(false);
                if (response.StatusCode == StatusCode.Success)
                {
                    _ = ProcessInMainThreadAsync(() => FullTree = new ObservableCollection<Departament>(response.Data));
                }
            });

        private async void OnReceiveMessageAsync(DepartamentMessage message) => 
            _ = ProcessInMainThreadAsync(() => FullTree.Add(message.Departament));

        private async void OnReceiveMessage(GroupMessage message) => _ = ProcessInMainThreadAsync(() =>
        {
            var departament = FullTree.FirstOrDefault(d => d.Id == message.Group.DepartamentId);
            departament?.Groups.Add(message.Group);
        });

        #endregion
    }
}
