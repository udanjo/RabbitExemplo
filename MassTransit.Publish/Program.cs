using MassTransit.Message;
using MassTransit.Publish.Consumer;
using MassTransit.Publish.Producer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MassTransit.Publish
{
    public class Program
    {
        private readonly IRabbitMqProducer<NotificationMessage> _publish;
        //private readonly IServiceCollection _servicesCollection;

        //public Program(IRabbitMqProducer<NotificationMessage> publish)
        //{
        //    _publish = publish;
        //}

        static async void Main()
        {
            Console.WriteLine("Publicando Mensagem no RabbitMq");

            //_publish = new IRabbitMqProducer<NotificationMessage>();

            var message = new NotificationMessage
            {
                Date = DateTime.Now,
                Message = "Enviado uma mensagem no rabbitMq"
            };

            await _publish.PublishAsync(message);

            Console.WriteLine("Publicando Mensagem no RabbitMq");
        }

       
    }
}