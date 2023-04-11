using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTool.Domain.Models;
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

        public WorkSpaceViewModel() 
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
            // design data.
            TreeView = new TreeViewViewModel();
            TreeView.SelectedItem = new Departament { Title = "Гумманитарный", Groups = Enumerable.Range(1, 25).Select(g => new Group { Title = $"Группа {g}", Students = new Student[14] }).ToList() };
        }

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
