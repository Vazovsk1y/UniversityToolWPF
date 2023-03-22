using System.Collections.Generic;
using UniversityTool.Domain.Models;
using UniversityTool.ViewModels.Base;
using System;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Domain.Services.DataServices.Base;

namespace UniversityTool.ViewModels
{
    internal class StudentAddViewModel : DialogViewModel<IStudentAddWindowService>
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
            get => _selectedGroup ?? new();
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

        public StudentAddViewModel(IMessageBusService messageBus, IStudentAddWindowService windowService) : base(messageBus, windowService)
        { 
            WindowTitle = "Student Add";
        }

        #endregion

        #region --Commands--



        #endregion

        #region --Methods--

        protected override void OnAccepting(object action)
        {

        }

        #endregion
    }
}
