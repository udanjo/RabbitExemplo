using System;

namespace MassTransit.Message
{
    /// <summary>
    /// Namespace é importante sempre estar no mesmo nome, seja para publicar e consumer entender a mesma mensagem
    /// </summary>
    public class NotificationMessage
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}