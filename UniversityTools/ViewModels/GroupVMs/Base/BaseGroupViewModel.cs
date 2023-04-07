using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels.GroupVMs.Base
{
    internal abstract class BaseGroupViewModel<T> : DialogViewModel<T> where T : IBaseWindowService
    {
        #region --Fields--

        private string _groupName;
        private Departament _selectedDepartament;
        protected readonly IDepartamentService _departamentService;
        protected readonly IGroupService _groupService;

        #endregion

        #region --Properties--

        public string GroupName
        {
            get => _groupName;
            set => Set(ref _groupName, value);
        }

        public Departament SelectedDepartament
        {
            get => _selectedDepartament ?? new();
            set => Set(ref _selectedDepartament, value);
        }

        #endregion

        #region --Constructors--

        public BaseGroupViewModel() { }

        public BaseGroupViewModel(
            IMessageBusService messageBus,
            T windowService,
            IGroupService groupService,
            IDepartamentService departamentService) : base(messageBus, windowService)
        {
            _departamentService = departamentService;
            _groupService = groupService;
        }

        #endregion

        #region --Commands--



        #endregion

        #region --Methods--



        #endregion
    }
}
