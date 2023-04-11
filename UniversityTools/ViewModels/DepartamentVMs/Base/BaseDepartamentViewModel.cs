using UniversityTool.Domain.Services.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels.DepartamentVMs.Base
{
    internal abstract class BaseDepartamentViewModel<T> : DialogViewModel<T> where T : IBaseWindowService
    {
        #region --Fields--

        private string _departamentTitle;
        protected readonly IDepartamentService _departamentService;

        #endregion

        #region --Properties--

        public string DepartamentTitle
        {
            get => _departamentTitle;
            set => Set(ref _departamentTitle, value);
        }

        #endregion

        #region --Constructors--

        public BaseDepartamentViewModel()
        {

        }

        public BaseDepartamentViewModel(
            IMessageBusService messageBus,
            T windowService,
            IDepartamentService departamentService) : base(messageBus, windowService)
        {
            _departamentService = departamentService;
        }

        #endregion

        #region --Commands--



        #endregion

        #region --Methods--



        #endregion
    }
}
