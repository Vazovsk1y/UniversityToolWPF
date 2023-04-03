using System;
using System.Windows;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.ViewModels.Base;
using UniversityTool.ViewModels.ControlsViewModels;

namespace UniversityTool.ViewModels.DeletingVIewModels
{
    internal class DepartamentDeleteViewModel : BaseDepartamentViewModel<IDepartamentDeleteWindowService>
    {
        #region --Fields--

        private readonly TreeViewViewModel _tree;

        #endregion

        #region --Properties--

        public string Message => $"Are you sure that you want to delete \"{_tree.SelectedDepartament.Title}\" departament?";

        #endregion

        #region --Constructors--

        public DepartamentDeleteViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
            WindowTitle = "Departament Delete";
        }

        public DepartamentDeleteViewModel(IMessageBusService messageBus, 
            IDepartamentDeleteWindowService windowService, 
            IDepartamentService departamentService,
            TreeViewViewModel tree) : base(messageBus, windowService, departamentService)
        {
            _tree = tree;
            WindowTitle = "Departament Delete";
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object action)
        {
            var response = await _departamentService.Delete(_tree.SelectedDepartament);

            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new DepartamentMessage(response.Data, UIOperationTypeCode.Delete));
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _windowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                        });
                    }
                    break;
                case OperationResultStatusCode.Fail:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            _windowService.CloseWindow();
                            ShowMessageBox(response.Description, response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        });
                    }
                    break;
            }
        }

        protected override bool OnCanAccept(object p) => true;

        #endregion

        #region --Methods--



        #endregion
    }
}
