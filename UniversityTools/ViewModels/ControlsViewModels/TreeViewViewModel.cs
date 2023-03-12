﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models;
using UniversityTool.ViewModels.Base;
using UniversityTool.Domain.Services.DataServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityTool.ViewModels.ControlsViewModels
{
    internal class TreeViewViewModel : BaseViewModel
    {
        #region --Fields--

        private Student _selectedStudent;
        private Group _selectedGroup;
        private Departament _selectedDepartament;
        private ObservableCollection<Student> _students = new();
        private ObservableCollection<Group> _groups = new();
        private ObservableCollection<Departament> _departaments = new();
        private readonly ITreeDataRepositoryService _dataService;

        #endregion

        #region --Properties--

        public ObservableCollection<Student> Students
        {
            get => _students;
            set => _students = value;
        }

        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set => _groups = value;
        }

        public ObservableCollection<Departament> Departaments
        {
            get => _departaments;
            set => Set(ref _departaments, value);
        }

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value); 
        }

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set => Set(ref _selectedStudent, value);
        }

        public Departament SelectedDepartament
        {
            get => _selectedDepartament;
            set => Set(ref _selectedDepartament, value);
        }

        #endregion

        #region --Constructors--

        public TreeViewViewModel()
        {
            TreeViewItemSelectionChangedCommand = new RelayCommand(OnTreeViewItemSelectionChanged, OnCanSelectTreeViewItem);
        }

        public TreeViewViewModel(ITreeDataRepositoryService dataService) : this()
        {
            _dataService = dataService;
            InitializeFullTreeAsync();
        }

        #endregion

        #region --Commands--

        public ICommand TreeViewItemSelectionChangedCommand { get; private set; }

        private bool OnCanSelectTreeViewItem(object arg) => true;

        private void OnTreeViewItemSelectionChanged(object selectedItem)
        {
            // depends on selected item type
            if (selectedItem is Student student)
            {
                SelectedStudent = student;
            }
            else if (selectedItem is Group group)
            {
                SelectedGroup = group;
            }
            else if(selectedItem is Departament departament)
            {
                SelectedDepartament = departament;
            }
        }

        #endregion

        #region --Methods--

        private async void InitializeFullTreeAsync()
        {
            IEnumerable<Departament> departaments = await Task.Run(_dataService.GetFullTree).ConfigureAwait(false);
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Departaments = new ObservableCollection<Departament>(departaments);
            });
        }

        #endregion
    }
}
