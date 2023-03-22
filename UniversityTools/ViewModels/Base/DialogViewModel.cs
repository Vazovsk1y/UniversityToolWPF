using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UniversityTool.Domain.Messages.Base;
using UniversityTool.Domain.Services.Base;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Infastructure.Commands;

namespace UniversityTool.ViewModels.Base
{
    internal abstract class DialogViewModel<T> : TitledViewModel where T : IBaseWindowService
    {
        #region --Fields--

        protected readonly IMessageBusService _messageBusService;
        protected readonly T _windowService;

        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public DialogViewModel() 
        {
            AcceptCommand = new RelayCommand(OnAccepting, OnCanAccept);
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
        }

        public DialogViewModel(IMessageBusService messageBus, T windowService) : this()
        {
           
            _messageBusService = messageBus;
            _windowService = windowService;
        }

        #endregion

        #region --Commands--

        public ICommand CancelCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        protected virtual bool OnCanCancel(object p) => true;

        protected virtual void OnCanceling(object p) => _windowService.CloseWindow();

        protected virtual bool OnCanAccept(object p) => true;

        protected abstract void OnAccepting(object action);

        #endregion

        #region --Methods--

        protected virtual async Task SendMessageAsync<T>(T message) where T : BaseMessage =>
          await Task.Run(() => _messageBusService.Send(message)).ConfigureAwait(false);

        protected virtual void ShowMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage image) =>
            MessageBox.Show(message, caption, button, image);

        #endregion
    }
}
