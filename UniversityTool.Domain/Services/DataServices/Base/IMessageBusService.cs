namespace UniversityTool.Domain.Services.DataServices.Base
{
    /// <summary>
    /// Message bus for implementation sending messages from one window to another by publisher/subscriber, sender/receiver.
    /// </summary>
    public interface IMessageBusService
    {
        IDisposable RegisterHandler<T>(Action<T> handler);
        void Send<T>(T message);
    }
}
