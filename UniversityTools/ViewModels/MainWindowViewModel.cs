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
        }

        public MainWindowViewModel(
            IDepartamentAddWindowService departamentAddWindowService,
            IGroupAddWindowService groupAddWindowService, 
            TreeViewViewModel treeView) : this()
        {
            _groupAddWindow = groupAddWindowService;
            _departamentAddWindow = departamentAddWindowService;
            TreeView = treeView;
        }

        #endregion

        #region --Commands--

        public ICommand AddDepartamentCommand { get; }                          

        private bool OnCanAddDepartament(object arg) => true;

        private void OnAddingDepartament(object obj) => _ = ProcessInMainThreadAsync(_departamentAddWindow.OpenWindow);

        public ICommand AddGroupCommand { get; }                          

        private bool OnCanAddGroup(object arg) => true;

        private void OnAddingGroup(object obj) => _ = ProcessInMainThreadAsync(_groupAddWindow.OpenWindow);

        #endregion

        #region --Methods--
        


        #endregion
    }
}
