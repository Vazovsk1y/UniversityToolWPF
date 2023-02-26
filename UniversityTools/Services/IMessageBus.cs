using System;

namespace UniversityTool.Services
{
    public interface IMessageBus
    {
        IDisposable RegisterHandler<T>(Action<T> handler);
        void Send<T>(T message);
    }
}
