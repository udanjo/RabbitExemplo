using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Rabbit
{
    class Program
    {
        static int contador = 1;

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                VirtualHost = "atlas",
                HostName = "10.1.5.174",
                UserName = "atlas",
                Password = "iwz6w{?f"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;

                channel.BasicConsume(queue: "esc-atlas", autoAck: true, consumer);

                Console.WriteLine("Aguardando mensagens para processamento");
                Console.WriteLine("Pressione uma tecla para encerrar...");
                Console.ReadKey();
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            //var message = Encoding.UTF8.GetString(e.Body);
            var message = e.Body.ToString();
            Console.WriteLine(Environment.NewLine + $"[Nova mensagem recebida] - NUMERO: {contador++} " + message);
        }
    }
}
