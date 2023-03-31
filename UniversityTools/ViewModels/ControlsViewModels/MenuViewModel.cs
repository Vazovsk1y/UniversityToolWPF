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
        private readonly IStudentAddWindowService _studentAddWindow;
        private readonly IDepartamentUpdateWindowService _departamentUpdateWindow;
        private readonly IGroupUpdateWindowService _groupUpdateWindow;

        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public MenuViewModel(IDepartamentAddWindowService departamentAddWindowService,
            IGroupAddWindowService groupAddWindowService,
            IStudentAddWindowService studentAddWindowService,
            TreeViewViewModel tree,
            IDepartamentUpdateWindowService departamentUpdateWindowService,
            IGroupUpdateWindowService groupUpdateWindowService)
        {
            _groupUpdateWindow = groupUpdateWindowService;
            _departamentUpdateWindow = departamentUpdateWindowService;
            _departamentAddWindow = departamentAddWindowService;
            _groupAddWindow = groupAddWindowService;
            _studentAddWindow = studentAddWindowService;
            _tree = tree;
        }

        #endregion

        #region --Commands--

        public ICommand UpdateGroupCommand => new RelayCommand(OnGroupUpdating, OnCanGroupUpdate);

        private bool OnCanGroupUpdate(object arg) => _tree.SelectedGroup is not null;

        private void OnGroupUpdating(object obj) => _groupUpdateWindow.OpenWindow();

        public ICommand UpdateDepartamentCommand => new RelayCommand(OnDepartamentUpdating, OnCanUpdateDepartament);

        private bool OnCanUpdateDepartament(object arg) => _tree.SelectedDepartament is not null;

        private void OnDepartamentUpdating(object obj) => _departamentUpdateWindow.OpenWindow();

        public ICommand AddDepartamentCommand => new RelayCommand(OnDepartamentAdding);

        private void OnDepartamentAdding(object obj) => _ = ProcessInMainThreadAsync(_departamentAddWindow.OpenWindow);

        public ICommand AddGroupCommand => new RelayCommand(OnGroupAdding);

        private void OnGroupAdding(object obj) => _ = ProcessInMainThreadAsync(_groupAddWindow.OpenWindow);

        public ICommand AddStudentCommand => new RelayCommand(OnStudentAdding);

        private void OnStudentAdding(object obj) => _ = ProcessInMainThreadAsync(_studentAddWindow.OpenWindow);

        #endregion

        #region --Methods--



        #endregion
    }
}
