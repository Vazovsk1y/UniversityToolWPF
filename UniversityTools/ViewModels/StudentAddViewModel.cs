using System.Collections.Generic;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.ViewModels.Base;
using System;

namespace UniversityTool.ViewModels
{
    internal class StudentAddViewModel : TitledViewModel
    {
        #region --Fields--

        private IEnumerable<Group> _groups;
        private Group _selectedGroup;
        private string _studentName;
        private string _studentSurname;
        private string _studentThirdName;

        #endregion

        #region --Properties--

        public string StudentName
        {
            get => _studentName;
            set => Set(ref _studentName, value);
        }

        public string StudentSurname
        {
            get => _studentSurname;
            set => Set(ref _studentSurname, value);
        }

        public string StudentThirdName
        {
            get => _studentThirdName;
            set => Set(ref _studentThirdName, value);
        }

        public IEnumerable<Group> Groups 
        { 
            get => _groups; 
            set => Set(ref _groups, value); 
        }

        public Group SelectedGroup 
        { 
            get => _selectedGroup; 
            set => Set(ref _selectedGroup, value); 
        }

        #endregion

        #region --Constructors--

        public StudentAddViewModel() 
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Constructor only for designTime");
            WindowTitle = "Student Add";
        }

        #endregion

        #region --Commands--



        #endregion

        #region --Methods--



        #endregion
    }
}
