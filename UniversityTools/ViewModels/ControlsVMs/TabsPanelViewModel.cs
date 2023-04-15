using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Models;
using UniversityTool.Domain.Models.Base;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Infastructure.Commands;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels.ControlsVMs
{
    internal class TabsPanelViewModel : BaseViewModel, IDisposable
    {
        #region --Fields--

        private readonly ObservableCollection<BaseModel> _tabs = new();
        private readonly IDisposable _subscription;
        private readonly IMessageBusService _messageBus;

        #endregion

        #region --Properties--

        public ObservableCollection<BaseModel> Tabs => _tabs;

        public TreeViewViewModel Tree { get; }

        #endregion

        #region --Constructors--

        public TabsPanelViewModel()
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("The default constructor of this view model type is only for design time");

            Tabs.Add(new Departament { Title = "Departament" });
            Tabs.Add(new Group { Title = "Group" });
            Tabs.Add(new Student { Name = "Student", SecondName = "Super", ThirdName = "Gut" });
        }

        public TabsPanelViewModel(
            IMessageBusService messageBus, 
            TreeViewViewModel tree)
        {
            _messageBus = messageBus;
            _subscription = _messageBus.RegisterHandler<TabsPanelMessage>(OnReceiveMessage);
            Tree = tree;
        }

        #endregion

        #region --Commands--

        public ICommand CloseTabCommand => new RelayCommand((item) =>
        {
            if (item is BaseModel tab && Tabs.Contains(tab))
            {
                Tabs.Remove(tab);
            }
        });

        #endregion

        #region --Methods--

        public void Dispose() => _subscription.Dispose();

        private void OnReceiveMessage(TabsPanelMessage message)
        {
            if (message.Item is not BaseModel tab)
                return;

            switch (message.OperationType)
            {
                case UIOperationTypeCode.Add:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            if (!Tabs.Contains(tab))
                            {
                                Tabs.Add(tab);
                            }
                        });
                    }
                    return;
                case UIOperationTypeCode.Delete:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            if (Tabs.Contains(tab))
                            {
                                Tabs.Remove(tab);
                            }
                        });
                    }
                    return;
                case UIOperationTypeCode.Update:
                    return;
                case UIOperationTypeCode.Move:
                    return;
            }
        }

        #endregion

    }
}
