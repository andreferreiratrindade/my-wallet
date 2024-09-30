

using Framework.Core.Messages.Integration;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Framework.MessageBus

{
    public class MessageBus : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger _logger;

        public MessageBus(IPublishEndpoint publishEndpoint, ILogger<MessageBus> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger= logger;
        }
        public void Dispose()
        {

        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : IntegrationEvent
        {
            _logger.CreateLog(new GenericLog(message.CorrelationId,
                                             message.GetType().Name,
                                             [LogHelper.SEND_TO_BROKER],
                                             message));

            return _publishEndpoint.Publish(message,cancellationToken);
        }
    }

}
