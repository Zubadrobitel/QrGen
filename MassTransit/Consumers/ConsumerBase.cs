using MassTransit;
using Microsoft.Extensions.Logging;
using QrGen.Domain.Model.MassTransit;

namespace Transit.Consumers
{
    public class ConsumerBase<T> : IConsumer<BaseEvent<T>>
    {
        private readonly ILogger<ConsumerBase<T>> _logger;

        public ConsumerBase(ILogger<ConsumerBase<T>> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BaseEvent<T>> context)
        {
            var message = context.Message;
            _logger.LogInformation("Событие получено");
            await Task.CompletedTask;
        }
    }
}
