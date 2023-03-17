using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models;
using UniversityTool.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using UniversityTool.Domain.Services;
using System;
using UniversityTool.Domain.Repositories;
using UniversityTool.Domain.Messages;

namespace UniversityTool.ViewModels.ControlsViewModels
{
    internal class TreeViewViewModel : BaseViewModel, IDisposable
    {
        #region --Fields--

        private Student _selectedStudent;
        private Group _selectedGroup;
        private Departament _selectedDepartament;
        private ObservableCollection<Departament> _departaments = new();
        private readonly ITreeRepository _dataTreeService;
        private readonly IMessageBusService _messageBus;
        private readonly List<IDisposable> _subscriptions = new();

        #endregion

        #region --Properties--

        public ObservableCollection<Departament> Departaments
        {
            get => _departaments;
            private set => Set(ref _departaments, value);
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
                Departaments = new ObservableCollection<Departament>(departaments);
            }
            else
            {
                throw new InvalidOperationException("This constructor is only for design time");

            }
        }

        public TreeViewViewModel(ITreeRepository dataService, IMessageBusService messageBusService)
        {
            _messageBus = messageBusService;
            _dataTreeService = dataService;
            _subscriptions.Add(_messageBus.RegisterHandler<DepartamentMessage>(OnReceiveMessage));
            _subscriptions.Add(_messageBus.RegisterHandler<GroupMessage>(OnReceiveMessage));
            TreeViewItemSelectionChangedCommand = new RelayCommand(OnTreeViewItemSelectionChanged, OnCanSelectTreeViewItem);
            InitializeFullTreeAsync();
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

        private async void InitializeFullTreeAsync()
        {
            IEnumerable<Departament> departaments = await Task.Run(_dataTreeService.GetFullTree).ConfigureAwait(false);
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Departaments = new ObservableCollection<Departament>(departaments);
            });
        }

        private async void OnReceiveMessage(DepartamentMessage message)
        {
            if (message is null || message.Departament.Title is null) return;

            if (message.Departament.Title.Length != 0)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Departaments.Add(message.Departament);
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
                var departament = Departaments.FirstOrDefault(d => d.Id == group.DepartamentId);
                departament?.Groups.Add(group);
            });
        }

        #endregion
    }
}
