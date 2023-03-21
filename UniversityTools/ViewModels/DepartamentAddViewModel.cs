using System.Windows.Input;
using UniversityTool.Infastructure.Commands;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using UniversityTool.Domain.Models;
using System.Windows;
using System.Threading.Tasks;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Services.DataServices.Base;

namespace UniversityTool.ViewModels
{
    internal class DepartamentAddViewModel : TitledViewModel
    {
        #region --Fields--

        private string _departamentTitle;
        private readonly IMessageBusService _messageBus;
        private readonly IDepartamentAddWindowService _departamentAddWindowService;
        private readonly IDepartamentService _departamentService;

        #endregion

        #region --Properties--

        public string DepartamentTitle
        {
            get => _departamentTitle;
            set => Set(ref _departamentTitle, value);
        }

        #endregion

        #region --Constructors--

        public DepartamentAddViewModel()
        {
            WindowTitle = "Departament Window";
            AcceptCommand = new RelayCommand(OnAccepting, OnCanAccept);
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
        }

        public DepartamentAddViewModel(IDepartamentAddWindowService userDialog, IMessageBusService messageBus
            , IDepartamentService departamentService) : this()
        {
            _departamentService = departamentService;
            _departamentAddWindowService = userDialog;
            _messageBus = messageBus;
        }

        #endregion

        #region --Commands--

        public ICommand CancelCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        private bool OnCanCancel(object p) => true;

        private void OnCanceling(object p) => _departamentAddWindowService.CloseWindow();

        private bool OnCanAccept(object p) => true;

        private async void OnAccepting(object action)
        {
            var response = await _departamentService.Add(new Departament { Title = DepartamentTitle }).ConfigureAwait(false);
            switch (response.StatusCode)
            {
                case OperationStatusCode.Success:
                    {
                        _ = SendMessageAsync(response.Data);
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _departamentAddWindowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                        });
                        break;
                    }
                case OperationStatusCode.Fail:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _departamentAddWindowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        });
                        break;
                    }
            }
        }

        #endregion

        #region --Methods--

        private async Task SendMessageAsync(Departament entity) => await Task
            .Run(() => _messageBus
            .Send(new DepartamentMessage(entity, OperationTypeCode.Add)))
            .ConfigureAwait(false);

        private void ShowMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage image) =>
            MessageBox.Show(message, caption, button, image);

        #endregion
    }
}
