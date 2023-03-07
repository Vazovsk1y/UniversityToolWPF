using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Models;

namespace UniversityTool.ViewModels
{
    internal class DepartamentAddViewModel : BaseViewModel
    {
        #region --Fields--

        private string _departamentName;
        private readonly IMessageBusService _messageBus;
        private readonly IDepartamentAddWindowService _departamentAddService;
        private readonly IDataService<Departament> _dataProviderService;

        #endregion

        #region --Properties--

        
        public string DepartamentName
        {
            get { return _departamentName; }
            set { Set(ref _departamentName, value); }
        }

        #endregion

        #region --Constructors--

        public DepartamentAddViewModel()
        {
            AcceptCommand = new RelayCommand(OnAccepting, OnCanAccept);
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
        }

        public DepartamentAddViewModel(IDepartamentAddWindowService userDialog, IMessageBusService messageBus
            ,IDataService<Departament> dataProviderService) : this()
        {
            _dataProviderService = dataProviderService;
            _departamentAddService = userDialog;
            _messageBus = messageBus;
        }

        #endregion

        #region --Commands--

        public ICommand CancelCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        private bool OnCanCancel(object p) => true;

        private void OnCanceling(object p)
        {
            _departamentAddService.CloseWindow();
        }

        private bool OnCanAccept(object p) => true;

        private void OnAccepting(object a)
        {
            _messageBus.Send(new DepartamentTitleMessage(DepartamentName));
            _dataProviderService.Add(new Departament { Title = DepartamentName });
            _departamentAddService.CloseWindow();
        }

        #endregion

        #region --Methods--


        #endregion
    }
}
