using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.Domain.Models;
using System.Windows;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Services.DataServices.Base;
using System;
using UniversityTool.ViewModels.DepartamentVMs.Base;

namespace UniversityTool.ViewModels.DepartamentVMs
{
    internal class DepartamentAddViewModel : BaseDepartamentViewModel<IDepartamentAddWindowService>
    {
        #region --Fields--



        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public DepartamentAddViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
            WindowTitle = "Departament add Window";
        }

        public DepartamentAddViewModel(IDepartamentAddWindowService departamentAddWindowService, IMessageBusService messageBus
            , IDepartamentService departamentService) : base(messageBus, departamentAddWindowService, departamentService)
        {
            WindowTitle = "Departament add Window";
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
