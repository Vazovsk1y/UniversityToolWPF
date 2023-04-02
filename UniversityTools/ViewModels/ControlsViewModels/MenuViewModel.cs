using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using System;

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
        private readonly IStudentUpdateWindowService _studentUpdateWindow;
        private readonly IDepartamentDeleteWindowService _departamentDeleteWindow;

        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public MenuViewModel(IDepartamentAddWindowService departamentAddWindowService,
            IGroupAddWindowService groupAddWindowService,
            IStudentAddWindowService studentAddWindowService,
            TreeViewViewModel tree,
            IDepartamentUpdateWindowService departamentUpdateWindowService,
            IGroupUpdateWindowService groupUpdateWindowService,
            IStudentUpdateWindowService studentUpdateWindowService,
            IDepartamentDeleteWindowService departamentDeleteWindowService)
        {
            _studentUpdateWindow = studentUpdateWindowService;
            _groupUpdateWindow = groupUpdateWindowService;
            _departamentUpdateWindow = departamentUpdateWindowService;
            _departamentAddWindow = departamentAddWindowService;
            _groupAddWindow = groupAddWindowService;
            _studentAddWindow = studentAddWindowService;
            _tree = tree;
            _departamentDeleteWindow = departamentDeleteWindowService;
        }

        #endregion

        #region --Commands--

        public ICommand DeleteDepartamentCommand => new RelayCommand(OnDepartamentDeleting, OnCanDepartamentDelete);

        private bool OnCanDepartamentDelete(object arg) => _tree.SelectedDepartament is not null;

        private void OnDepartamentDeleting(object obj) => _departamentDeleteWindow.OpenWindow();

        public ICommand UpdateStudentCommand => new RelayCommand(OnStudentUpdating, OnCanStudentUpdate);

        private bool OnCanStudentUpdate(object arg) => _tree.SelectedStudent is not null;

        private void OnStudentUpdating(object obj) => _studentUpdateWindow.OpenWindow();

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
