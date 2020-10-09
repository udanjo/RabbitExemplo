using Newtonsoft.Json;
using RabbitMQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> listIds = GetInts();

            var factory = new ConnectionFactory
            {
                VirtualHost = "TMS",
                HostName = "*********",
                UserName = "username",
                Password = "password"
            };

            var listModel = new List<Model>();

            for (int i = 0; i < listIds.Count; i++)
            {
                var model = new Model
                {
                    Id = listIds[i],
                    IntegrationDate = DateTime.Now
                };
                listModel.Add(model);
            }

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                CreateQueueExchange(channel);

                foreach (var model in listModel)
                {
                    var rabbitModel = CreateRabbitModel(model);

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rabbitModel));

                    channel.BasicPublish(exchange: "",
                                        routingKey: "esc-atlas",
                                        basicProperties: null,
                                        body: body);
                }
            }

            Console.WriteLine("Mensagem encaminhada com sucesso.");
        }

        public static BaseRabbitModel<Model> CreateRabbitModel(Model message)
        {
            var rabbitModel = new BaseRabbitModel<Model>
            {
                MessageType = new List<string> { "urn:message:Rodonaves.Messages.Contracts:ITrafficScheduleEvent" },
                DestinationAddress = "rabbitmq://{IP}/esc-event",
                Message = message,
                Headers = new object()
            };

            return rabbitModel;
        }

        public static void CreateQueueExchange(IModel channel)
        {
            channel.ExchangeDeclare("esc-event", "fanout");

            var maxPriority = 0;

            var argumments = new Dictionary<string, object>
            {
                { "x-max-priority", maxPriority }
            };

            channel.QueueDeclare(queue: "esc-atlas",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: argumments);

            channel.QueueBind("esc-atlas", "esc-event", "");
        }

        public static List<int> GetInts()
        {
            List<int> listIds = new List<int>
            {
                    3516594,
                    3516595,
                    3516596
            };

            return listIds;
        }
    }
}
