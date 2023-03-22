using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.ViewModels.Base;
using UniversityTool.Domain.Models;
using System.Windows;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Services.DataServices.Base;
using System;

namespace UniversityTool.ViewModels
{
    internal class DepartamentAddViewModel : DialogViewModel<IDepartamentAddWindowService>
    {
        #region --Fields--

        private string _departamentTitle;
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
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Standart constructor is only for design time");
            WindowTitle = "Departament Window";
        }

        public DepartamentAddViewModel(IDepartamentAddWindowService departamentAddWindowService, IMessageBusService messageBus
            , IDepartamentService departamentService) : base(messageBus, departamentAddWindowService)
        {
            WindowTitle = "Departament Window";
            _departamentService = departamentService;
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object action)
        {
            var response = await _departamentService.Add(new Departament { Title = DepartamentTitle }).ConfigureAwait(false);
            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new DepartamentMessage(response.Data, UIOperationTypeCode.Add));
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _windowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                        });
                        break;
                    }
                case OperationResultStatusCode.Fail:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _windowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        });
                        break;
                    }
            }
        }

        #endregion

        #region --Methods--



        #endregion
    }
}
