using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Models.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.DataServices.Base;
using System.Windows;
using System.Threading.Tasks;

namespace UniversityTool.ViewModels
{
    internal class DepartamentAddViewModel : TitledViewModel
    {
        #region --Fields--

        private string _departamentName;
        private readonly IMessageBusService _messageBus;
        private readonly IDepartamentAddWindowService _departamentAddService;
        private readonly IBaseRepository<Departament> _dataProviderService;

        #endregion

        #region --Properties--

        public string DepartamentName
        {
            get => _departamentName; 
            set => Set(ref _departamentName, value);
        }

        #endregion

        #region --Constructors--

        public DepartamentAddViewModel()
        {
            Title = "Departament Window";
            AcceptCommand = new RelayCommand(OnAccepting, OnCanAccept);
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
        }

        public DepartamentAddViewModel(IDepartamentAddWindowService userDialog, IMessageBusService messageBus
            ,IBaseRepository<Departament> dataProviderService) : this()
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

        private void OnCanceling(object p) => _departamentAddService.CloseWindow();

        private bool OnCanAccept(object p) => true;

        private async void OnAccepting(object action)
        {
            var entity = await _dataProviderService.Add(new Departament { Title = DepartamentName }).ConfigureAwait(false);
            await SendMessageAndCloseWindowAsync(entity);

            //await Application.Current.Dispatcher.InvokeAsync(() =>
            //{
            //    _messageBus.Send(new DepartamentMessage(entity));
            //    _departamentAddService.CloseWindow();
            //});
        }

        #endregion

        #region --Methods--

        private async Task SendMessageAndCloseWindowAsync(Departament entity)
        {
            await Task.Run(() =>
            {
                _messageBus.Send(new DepartamentMessage(entity));
            }).ConfigureAwait(false);

            await Application.Current.Dispatcher.InvokeAsync(_departamentAddService.CloseWindow);
        }

        #endregion
    }
}
