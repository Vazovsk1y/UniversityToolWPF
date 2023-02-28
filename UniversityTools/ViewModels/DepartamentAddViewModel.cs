using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Models;
using UniversityTool.Models.Messages;
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

        public ICommand AcceptCommand { get; private set; }
        
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
        }

        public DepartamentAddViewModel(IDepartamentAddService userDialog, IMessageBus messageBus) : this()
        {
            _departamentAddService = userDialog;
            _messageBus = messageBus;
        }

        #endregion

        #region --Methods--

        private bool OnCanAccept(object p) => true;

        private void OnAccepting(object a)
        {
            _messageBus.Send(new DepartamentAddMessage(new Departament { Title = DepartamentName }));
            _departamentAddService.CloseWindow();
        }

        #endregion
    }
}
