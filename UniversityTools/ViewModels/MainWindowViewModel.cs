using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;

namespace UniversityTool.ViewModels
{
    internal class MainWindowViewModel : TitledViewModel
    {
        #region --Fields--

        

        #endregion

        #region --Properties--                 

        public TreeViewViewModel TreeView { get; }

        public MenuViewModel Menu { get; }

        #endregion

        #region --Constructors--

        public MainWindowViewModel()
        {
            WindowTitle = "UniversityTool";
        }

        public MainWindowViewModel(TreeViewViewModel treeViewViewModel, MenuViewModel menuViewModel)
        {
            Menu = menuViewModel;
            TreeView = treeViewViewModel;
            WindowTitle = "UniversityTool";
        }

        #endregion

        #region --Commands--

        

        #endregion

        #region --Methods--



        #endregion
    }
}
