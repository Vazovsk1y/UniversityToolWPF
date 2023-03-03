using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models.Messages;
using UniversityTool.Services;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels
{
    internal class DepartamentAddViewModel : BaseViewModel
    {
        #region --Fields--

        private string _departamentName;
        private readonly IMessageBus _messageBus;
        private readonly IDepartamentAddService _departamentAddService;

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

        public DepartamentAddViewModel(IDepartamentAddService userDialog, IMessageBus messageBus) : this()
        {
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
            _departamentAddService.CloseWindow();
        }

        #endregion

        #region --Methods--


        #endregion
    }
}
