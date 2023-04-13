using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversityTool.Domain.Codes;
using UniversityTool.Domain.Messages;
using UniversityTool.Domain.Services.DataServices.Base;
using UniversityTool.Infastructure.Commands;
using UniversityTool.ViewModels.Base;

namespace UniversityTool.ViewModels.ControlsVMs
{
    internal class TabsPanelViewModel : BaseViewModel, IDisposable
    {
        #region --Fields--

        private readonly ObservableCollection<object> _tabs = new();
        private readonly IDisposable _subscription;
        private readonly IMessageBusService _messageBus;

        #endregion

        #region --Properties--

        public ObservableCollection<object> Tabs => _tabs;

        public TreeViewViewModel Tree { get; }

        #endregion

        #region --Constructors--

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



        #endregion

        #region --Methods--

        public void Dispose() => _subscription.Dispose();

        private void OnReceiveMessage(TabsPanelMessage message)
        {
            switch (message.OperationType)
            {
                case UIOperationTypeCode.Add:
                    {
                        _ = ProcessInMainThreadAsync(() =>
                        {
                            if (message.Item is not null && !Tabs.Contains(message.Item))
                            {
                                Tabs.Add(message.Item);
                            }
                        });
                    }
                    break;
                case UIOperationTypeCode.Delete:
                    break;
                case UIOperationTypeCode.Update:
                    break;
                case UIOperationTypeCode.Move:
                    break;
            }
        }

        #endregion

    }
}
