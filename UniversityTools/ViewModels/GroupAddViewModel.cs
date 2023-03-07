using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Models.Messages;
using UniversityTool.Domain.Services;
using UniversityTool.Domain.Services.DataServices;
using UniversityTool.Domain.Services.WindowsServices;
using UniversityTool.Infastructure.Commands;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels
{
    internal class GroupAddViewModel : BaseViewModel
    {
        #region --Fields--

        private ObservableCollection<Departament> _departaments;
        private readonly IDataService<Departament> _dataService;
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

        public GroupAddViewModel(IDataService<Departament> dataService, IGroupAddWindowService groupAddWindowService) : this()
        {
            _dataService = dataService;
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

        private void InitializeDepartamentsAsync()
        {
            Task.Run(async () =>
            {
                IEnumerable<Departament> departaments = await _dataService.GetAll();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Departaments = new ObservableCollection<Departament>(departaments);
                });
            });
        }

        #endregion
    }
}
