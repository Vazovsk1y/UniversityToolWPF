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
            Title = "Departament Window";
            AcceptCommand = new RelayCommand(OnAccepting, OnCanAccept);
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
        }

        public DepartamentAddViewModel(IDepartamentAddWindowService userDialog, IMessageBusService messageBus
            ,IDepartamentService departamentService) : this()
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
            var response = await _departamentService.AddDepartament(DepartamentTitle).ConfigureAwait(false);
            if (response.StatusCode == StatusCode.Success)
            {
                await SendMessageAndCloseWindowAsync(response.Data);
                MessageBox.Show(response.Description);
            }
            else
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show(response.Description);
                    _departamentAddWindowService.CloseWindow();
                });
            }
        }

        #endregion

        #region --Methods--

        private async Task SendMessageAndCloseWindowAsync(Departament entity)
        {
            await Task.Run(() =>
            {
                _messageBus.Send(new DepartamentMessage(entity));
            }).ConfigureAwait(false);

            await Application.Current.Dispatcher.InvokeAsync(_departamentAddWindowService.CloseWindow);
        }

        #endregion
    }
}
