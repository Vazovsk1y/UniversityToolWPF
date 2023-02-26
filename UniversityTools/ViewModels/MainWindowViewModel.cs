using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;

namespace UniversityTool.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region --Fields--


        #endregion

        #region --Properties--

        public TreeViewViewModel TreeViewViewModel { get; }

        #endregion

        #region --Constructors--

        public MainWindowViewModel() 
        {
            TreeViewViewModel = new TreeViewViewModel();
        }

        #endregion
    }
}
