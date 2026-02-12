using QrGen.Domain.Model.MassTransit;

namespace QrGen.Domain.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishEventAsync<T>(BaseEvent<T> transitEvent);
    }
}
