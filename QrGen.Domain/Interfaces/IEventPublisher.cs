using QrGen.Domain.Model.MassTransit;

namespace QrGen.Domain.Interfaces
{
    public interface IEventPublisher
    {
        Task PulishEvent<T>(BaseEvent<T> transitEvent);
    }
}
