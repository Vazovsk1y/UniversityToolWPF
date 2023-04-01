using System.Linq;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;

namespace UniversityTool.ViewModels.Base
{
    internal abstract class BaseStudentViewModel<T> : DialogViewModel<T> where T : IBaseWindowService
    {
        #region --Fields--

        private string _studentName;
        private string _studentSurname;
        private string _studentThirdName;
        private Group _selectedGroup;
        protected readonly IStudentService _studentService;
        protected readonly IGroupService _groupService;

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

        public Group SelectedGroup
        {
            get => _selectedGroup ?? new();
            set => Set(ref _selectedGroup, value);
        }

        #endregion

        #region --Constructors--

        public BaseStudentViewModel() { }

        public BaseStudentViewModel(IMessageBusService messageBus, 
            T windowService, 
            IGroupService groupService,
            IStudentService studentService) : base(messageBus, windowService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        #endregion

        #region --Commands--

        protected override bool OnCanAccept(object p) => IsAcceptButtonEnable(StudentName, StudentSurname, StudentThirdName);

        #endregion

        #region --Methods--

        private bool IsAcceptButtonEnable(params string[] args) => args.ToList().TrueForAll(row => row is not null && row.Length > 0);

        #endregion
    }
}
