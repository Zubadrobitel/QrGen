using MassTransit;
using Microsoft.Extensions.Logging;
using QrGen.Domain.Interfaces;
using QrGen.Domain.Model.MassTransit;
using Transit.Enums;

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
        public async Task PulishEvent<T>(BaseEvent<T> transitEvent)
        {
            switch (transitEvent.Method)
            {
                default:
            }
        }
    }
}
