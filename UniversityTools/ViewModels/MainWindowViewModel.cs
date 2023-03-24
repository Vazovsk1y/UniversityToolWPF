using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;
using UniversityTool.Domain.Services.WindowsServices;

namespace UniversityTool.ViewModels
{
    internal class MainWindowViewModel : TitledViewModel
    {
        #region --Fields--

        private readonly IDepartamentAddWindowService _departamentAddWindow;
        private readonly IGroupAddWindowService _groupAddWindow;
        private readonly IStudentAddWindowService _studentAddWindowService;

        #endregion

        #region --Properties--                 

        public TreeViewViewModel TreeView { get; }

        #endregion

        #region --Constructors--

        public MainWindowViewModel() 
        {
            WindowTitle = "UniversityTool";
            AddDepartamentCommand = new RelayCommand(OnAddingDepartament, OnCanAddDepartament);
            AddGroupCommand = new RelayCommand(OnAddingGroup, OnCanAddGroup);
            AddStudentCommand = new RelayCommand(OnStudentAdding, OnCanAddStudent);
        }

        public MainWindowViewModel(
            IDepartamentAddWindowService departamentAddWindowService,
            IGroupAddWindowService groupAddWindowService, 
            TreeViewViewModel treeView,
            IStudentAddWindowService studentAddWindowService) : this()
        {
            _groupAddWindow = groupAddWindowService;
            _departamentAddWindow = departamentAddWindowService;
            TreeView = treeView;
            _studentAddWindowService = studentAddWindowService;
        }

        #endregion

        #region --Commands--

        public ICommand AddDepartamentCommand { get; }                          

        private bool OnCanAddDepartament(object arg) => true;

        private void OnAddingDepartament(object obj) => _ = ProcessInMainThreadAsync(_departamentAddWindow.OpenWindow);

        public ICommand AddGroupCommand { get; }                          

        private bool OnCanAddGroup(object arg) => true;

        private void OnAddingGroup(object obj) => _ = ProcessInMainThreadAsync(_groupAddWindow.OpenWindow);

        public ICommand AddStudentCommand { get; }

        private bool OnCanAddStudent(object arg) => true;

        private void OnStudentAdding(object obj) => _ = ProcessInMainThreadAsync(_studentAddWindowService.OpenWindow);

        #endregion

        #region --Methods--



        #endregion
    }
}
