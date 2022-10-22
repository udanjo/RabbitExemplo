using MassTransit.Message;
using System;
using System.Threading.Tasks;

namespace MassTransit.Api.Consumer
{
    public class ReceiverConsumer : IConsumer<NotificationMessage>
    {
        public async Task Consume(ConsumeContext<NotificationMessage> context)
        {
            Console.WriteLine("Mensagem consumida... Segue as informações:");
            Console.WriteLine($" Data de Envio: {context.Message.Date}");
            Console.WriteLine($" Mensagem: {context.Message.Message}");
        }
    }
}