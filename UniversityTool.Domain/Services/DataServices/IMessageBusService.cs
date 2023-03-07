namespace UniversityTool.Domain.Services
{
    public interface IMessageBusService
    {
        IDisposable RegisterHandler<T>(Action<T> handler);
        void Send<T>(T message);
    }
}
