using System.Collections.Generic;
using System.Threading.Tasks;

namespace MassTransit.Api.Producer
{
    public interface IRabbitMqProducer<in T> where T : class
    {
        Task PublishAsync(T @event);

        Task PublishBatchAsync(IEnumerable<T> @event);

        Task SendAsync(string address, T command);

        Task SendBatchAsync(string address, IEnumerable<T> command);
    }
}