namespace UniversityTool.Domain.Services.DataServices.Base
{
    public interface IMessageBusService
    {
        IDisposable RegisterHandler<T>(Action<T> handler);
        void Send<T>(T message);
    }
}
