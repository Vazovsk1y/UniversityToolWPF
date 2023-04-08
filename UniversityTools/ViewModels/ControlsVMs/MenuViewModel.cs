using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using System;

namespace UniversityTool.ViewModels.ControlsVMs
{
    internal class MenuViewModel : BaseViewModel
    {
        #region --Fields--

        private readonly TreeViewViewModel _tree;
        private readonly IDepartamentAddWindowService _departamentAddWindow;
        private readonly IDepartamentUpdateWindowService _departamentUpdateWindow;
        private readonly IDepartamentDeleteWindowService _departamentDeleteWindow;
        private readonly IGroupAddWindowService _groupAddWindow;
        private readonly IGroupUpdateWindowService _groupUpdateWindow;
        private readonly IGroupDeleteWindowService _groupDeleteWindow;
        private readonly IStudentAddWindowService _studentAddWindow;
        private readonly IStudentUpdateWindowService _studentUpdateWindow;
        private readonly IStudentDeleteWindowService _studentDeleteWindow;

        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public MenuViewModel(
            IDepartamentAddWindowService departamentAddWindowService,
            IGroupAddWindowService groupAddWindowService,
            IStudentAddWindowService studentAddWindowService,
            IDepartamentUpdateWindowService departamentUpdateWindowService,
            IGroupUpdateWindowService groupUpdateWindowService,
            IStudentUpdateWindowService studentUpdateWindowService,
            IDepartamentDeleteWindowService departamentDeleteWindowService,
            IGroupDeleteWindowService groupDeleteWindowService,
            IStudentDeleteWindowService studentDeleteWindowService,
            TreeViewViewModel tree)
        {
            _studentUpdateWindow = studentUpdateWindowService;
            _groupUpdateWindow = groupUpdateWindowService;
            _departamentUpdateWindow = departamentUpdateWindowService;
            _departamentAddWindow = departamentAddWindowService;
            _groupAddWindow = groupAddWindowService;
            _studentAddWindow = studentAddWindowService;
            _departamentDeleteWindow = departamentDeleteWindowService;
            _groupDeleteWindow = groupDeleteWindowService;
            _studentDeleteWindow = studentDeleteWindowService;
            _tree = tree;
        }

        #endregion

        #region --Commands--

        public ICommand AddDepartamentCommand => new RelayCommand((arg) => _departamentAddWindow.OpenWindow());

        public ICommand UpdateDepartamentCommand => new RelayCommand(
            (arg) => _departamentUpdateWindow.OpenWindow(),
            (arg) => _tree.SelectedDepartament is not null);

        public ICommand DeleteDepartamentCommand => new RelayCommand(
            (arg) => _departamentDeleteWindow.OpenWindow(),
            (arg) => _tree.SelectedDepartament is not null);

        public ICommand AddGroupCommand => new RelayCommand((arg) => _groupAddWindow.OpenWindow());

        public ICommand UpdateGroupCommand => new RelayCommand(
           (arg) => _groupUpdateWindow.OpenWindow(),
           (arg) => _tree.SelectedGroup is not null);

        public ICommand DeleteGroupCommand => new RelayCommand(
            (arg) => _groupDeleteWindow.OpenWindow(), 
            (arg) => _tree.SelectedGroup is not null);

        public ICommand AddStudentCommand => new RelayCommand((arg) => _studentAddWindow.OpenWindow());

        public ICommand UpdateStudentCommand => new RelayCommand(
            (arg) => _studentUpdateWindow.OpenWindow(),
            (arg) => _tree.SelectedStudent is not null);

        public ICommand DeleteStudentCommand => new RelayCommand(
            (arg) => _studentDeleteWindow.OpenWindow(),
            (arg) => _tree.SelectedStudent is not null);

        #endregion

        #region --Methods--



        #endregion
    }
}
