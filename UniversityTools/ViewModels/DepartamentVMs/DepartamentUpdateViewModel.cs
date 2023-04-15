using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using System;
using UniversityTool.ViewModels.ControlsVMs;
using UniversityTool.Domain.Models;
using System.Windows;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using UniversityTool.ViewModels.DepartamentVMs.Base;

namespace UniversityTool.ViewModels.DepartamentVMs
{
    internal class DepartamentUpdateViewModel : BaseDepartamentViewModel<IDepartamentUpdateWindowService>
    {
        #region --Fields--

        private readonly TreeViewViewModel _tree;

        #endregion

        #region --Properties--



        #endregion

        #region --Constructors--

        public DepartamentUpdateViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");
            WindowTitle = "Departament Update Window";
        }

        public DepartamentUpdateViewModel(
           IMessageBusService messageBus,
           IDepartamentUpdateWindowService windowService,
           IDepartamentService departamentService,
           TreeViewViewModel tree) : base(messageBus, windowService, departamentService)
        {
            _tree = tree;
            DepartamentTitle = _tree.SelectedDepartament.Title;
            WindowTitle = "Departament Update Window";
        }

        #endregion

        #region --Commands--

        protected override async void OnAccepting(object action)
        {
            string updatedTitle = DepartamentTitle;
            var response = await _departamentService.Update(new Departament
            {
                Title = updatedTitle,                                        
                Id = _tree.SelectedDepartament.Id,                              
                DateAdded = _tree.SelectedDepartament.DateAdded,
                Groups = _tree.SelectedDepartament.Groups
            }).ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case OperationResultStatusCode.Success:
                    {
                        _ = SendMessageAsync(new DepartamentMessage(response.Data, UIOperationTypeCode.Update));
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
