using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MassTransit.Api.Producer
{
    public class RabbitMqProducer<T> : IRabbitMqProducer<T> where T : class
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpointProvider _endpointProvider;

        public RabbitMqProducer(IPublishEndpoint publishEndpoint, ISendEndpointProvider endpointProvider)
        {
            _publishEndpoint = publishEndpoint;
            _endpointProvider = endpointProvider;
        }

        #region Publish

        public async Task PublishAsync(T @event)
        {
            ValidPublishAsync(@event);
            await _publishEndpoint.Publish(@event);
        }

        public async Task PublishBatchAsync(IEnumerable<T> @event)
        {
            ValidPublishBatchAsync(@event);
            await _publishEndpoint.PublishBatch(@event);
        }

        private static void ValidPublishAsync(T @event)
        {
            if (@event is null)
                throw new ArgumentNullException(nameof(@event));
        }

        private static void ValidPublishBatchAsync(IEnumerable<T> @event)
        {
            if (@event is null)
                throw new ArgumentNullException(nameof(@event));
        }

        #endregion Publish

        #region Send

        public async Task SendAsync(string address, T command)
        {
            ValidSend(address, command);
            var endpoint = await _endpointProvider.GetSendEndpoint(new Uri($"queue:{address}"));
            await endpoint.Send(command);
        }

        public async Task SendBatchAsync(string address, IEnumerable<T> command)
        {
            ValidSendBatch(address, command);
            var endpoint = await _endpointProvider.GetSendEndpoint(new Uri($"queue:{address}"));
            await endpoint.SendBatch(command);
        }

        private static void ValidSend(string address, T command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (string.IsNullOrEmpty(address))
                throw new ArgumentException(nameof(address));
        }

        private static void ValidSendBatch(string address, IEnumerable<T> command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (string.IsNullOrEmpty(address))
                throw new ArgumentException(nameof(address));
        }

        #endregion Send
    }
}