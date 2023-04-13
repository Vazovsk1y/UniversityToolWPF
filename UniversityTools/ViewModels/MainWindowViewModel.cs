using System;
using System.Collections.ObjectModel;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsVMs;

namespace UniversityTool.ViewModels
{
    internal class MainWindowViewModel : TitledViewModel
    {
        #region --Fields--



        #endregion

        #region --Properties--                 

        public TreeViewViewModel TreeView { get; }

        public MenuViewModel Menu { get; }

        public WorkSpaceViewModel WorkSpace { get; }

        public TabsPanelViewModel TabsPanel { get; }

        #endregion

        #region --Constructors--

        public MainWindowViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
            WindowTitle = "UniversityTool";
        }

        public MainWindowViewModel(
            TreeViewViewModel treeViewViewModel, 
            MenuViewModel menuViewModel,
            WorkSpaceViewModel workSpaceModel,
            TabsPanelViewModel tabsPanelViewModel)
        {
            Menu = menuViewModel;
            TreeView = treeViewViewModel;
            WorkSpace = workSpaceModel;
            TabsPanel = tabsPanelViewModel;
            WindowTitle = "UniversityTool";
        }

        #endregion

        #region --Commands--

        

        #endregion

        #region --Methods--



        #endregion
    }
}
