using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Infastructure.Commands;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels
{
    internal class GroupAddViewModel : BaseViewModel
    {
        #region --Fields--

        private ObservableCollection<Departament> _departaments;
        private readonly IBaseDataRepositoryService<Departament> _dataDepartamentService;
        private readonly IGroupAddWindowService _groupAddWindowService;

        #endregion

        #region --Properties--

        public ObservableCollection<Departament> Departaments
        {
            get => _departaments;
            set => Set(ref _departaments, value);                         
        }                                                                 
                                                                          
        #endregion                                                        

        #region --Constructors--

        public GroupAddViewModel()
        {
            CancelCommand = new RelayCommand(OnCanceling, OnCanCancel);
            AcceptCommand = new RelayCommand(OnAccepting, OnCanAccept);
        }

        public GroupAddViewModel(IBaseDataRepositoryService<Departament> dataService, IGroupAddWindowService groupAddWindowService) : this()
        {
            _dataDepartamentService = dataService;
            _groupAddWindowService = groupAddWindowService;
            InitializeDepartamentsAsync();
        }

        #endregion

        #region --Commands--

        public ICommand CancelCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        private bool OnCanCancel(object p) => true;

        private void OnCanceling(object p)
        {
            _groupAddWindowService.CloseWindow();
        }

        private bool OnCanAccept(object p) => true;

        private void OnAccepting(object a)
        {
            _groupAddWindowService.CloseWindow();
        }

        #endregion

        #region --Methods--

        private async void InitializeDepartamentsAsync()
        {
            IEnumerable<Departament> departaments = await Task.Run(_dataDepartamentService.GetAll).ConfigureAwait(false);
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Departaments = new ObservableCollection<Departament>(departaments);
            });
        }

        //private void InitializeDepartamentsAsync()
        //{
        //    Task.Run(async () =>
        //    {
        //        IEnumerable<Departament> departaments = await _dataService.GetAll().ConfigureAwait(false);
        //        await Application.Current.Dispatcher.InvokeAsync(() =>
        //        {
        //            Departaments = new ObservableCollection<Departament>(departaments);
        //        });
        //    });
        //}

        #endregion
    }
}
