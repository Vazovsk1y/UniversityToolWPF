using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels.ControlsVMs
{
    internal class WorkSpaceViewModel : BaseViewModel
    {
        #region --Fields--



        #endregion

        #region --Properties--

        public TreeViewViewModel TreeView { get; }

        #endregion

        #region --Constructors--

        public WorkSpaceViewModel() { }

        public WorkSpaceViewModel(
            TreeViewViewModel treeView)
        {
            TreeView = treeView;
        }

        #endregion

        #region --Commands--



        #endregion

        #region --Methods--



        #endregion
    }
}
