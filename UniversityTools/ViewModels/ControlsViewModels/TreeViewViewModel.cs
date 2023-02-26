using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Models;
using UniversityTool.ViewModels.Base;

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

        #endregion

        #region --Properties--
        public ICommand TreeViewItemSelectionChangedCommand { get; private set; }

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
            set => _departaments = value;
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

        #endregion

        #region --Commands--

        private bool OnCanSelectTreeViewItem(object arg) => true;

        private void OnTreeViewItemSelectionChanged(object selectedItem)
        {
            // 
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
    }
}
