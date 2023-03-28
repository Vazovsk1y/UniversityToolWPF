using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;

namespace UniversityTool.ViewModels.ControlsViewModels
{
    internal class MenuViewModel : BaseViewModel
    {
        #region --Fields--

        private readonly TreeViewViewModel _tree;
        private readonly IDepartamentAddWindowService _departamentAddWindow;
        private readonly IGroupAddWindowService _groupAddWindow;
        private readonly IStudentAddWindowService _studentAddWindowService;
        private readonly IDepartamentUpdateWindowService _departamentUpdateWindowService;

        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public MenuViewModel(IDepartamentAddWindowService departamentAddWindow,
            IGroupAddWindowService groupAddWindow,
            IStudentAddWindowService studentAddWindowService,
            TreeViewViewModel tree,
            IDepartamentUpdateWindowService departamentUpdateWindowService)
        {
            _departamentUpdateWindowService = departamentUpdateWindowService;
            _tree = tree;
            _departamentAddWindow = departamentAddWindow;
            _groupAddWindow = groupAddWindow;
            _studentAddWindowService = studentAddWindowService;
        }

        #endregion

        #region --Commands--

        public ICommand UpdateDepartamentCommand => new RelayCommand(OnUpdatingDepartament, OnCanUpdateDepartament);

        private bool OnCanUpdateDepartament(object arg) => _tree.SelectedDepartament is not null;

        private void OnUpdatingDepartament(object obj) => _departamentUpdateWindowService.OpenWindow();

        public ICommand AddDepartamentCommand => new RelayCommand(OnAddingDepartament);

        private void OnAddingDepartament(object obj) => _ = ProcessInMainThreadAsync(_departamentAddWindow.OpenWindow);

        public ICommand AddGroupCommand => new RelayCommand(OnAddingGroup);

        private void OnAddingGroup(object obj) => _ = ProcessInMainThreadAsync(_groupAddWindow.OpenWindow);

        public ICommand AddStudentCommand => new RelayCommand(OnStudentAdding);

        private void OnStudentAdding(object obj) => _ = ProcessInMainThreadAsync(_studentAddWindowService.OpenWindow);

        #endregion

        #region --Methods--



        #endregion
    }
}
