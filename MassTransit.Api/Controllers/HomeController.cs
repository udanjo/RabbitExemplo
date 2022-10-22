using MassTransit.Api.Producer;
using MassTransit.Message;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MassTransit.Api.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        public readonly IRabbitMqProducer<NotificationMessage> _publish;

        public HomeController(IRabbitMqProducer<NotificationMessage> publish)
        {
            _publish = publish;
        }

        [HttpPost]
        public async Task<IActionResult> Get()
        {
            Console.WriteLine("Montando Mensagem...");

            var message = new NotificationMessage
            {
                Date = DateTime.Now,
                Message = "Envio de mensagem pelo RabbitMq com MassTransit"
            };

            Console.WriteLine("Enviando Mensagem...");

            await _publish.PublishAsync(message);


            Console.WriteLine("Mensagem enviada com sucesso...");

            return Ok();
        }
    }
}