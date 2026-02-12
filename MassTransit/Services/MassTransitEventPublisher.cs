using MassTransit;
using Microsoft.Extensions.Logging;
using QrGen.Domain.Interfaces;
using QrGen.Domain.Model.MassTransit;

namespace Transit.Services
{
    public class MassTransitEventPublisher : IEventPublisher
    {
        public readonly IBus _bus;
        public readonly ILogger<MassTransitEventPublisher> _logger;

        public MassTransitEventPublisher(IBus bus, ILogger<MassTransitEventPublisher> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        public async Task PublishEventAsync<T>(BaseEvent<T> transitEvent)
        {
            await _bus.Publish(transitEvent);
            _logger.LogInformation($"Сервис: {transitEvent.Source}, метод: {transitEvent.Method}, отправлено!");
        }
    }
}