using Newtonsoft.Json;
using System.Collections.Generic;

namespace Rabbit.Publish
{
    public class BaseRabbitModel<T>
    {
        [JsonProperty("destinationAddress")]
        public string DestinationAddress { get; set; }

        [JsonProperty("message")]
        public T Message { get; set; }

        [JsonProperty("headers")]
        public object Headers { get; set; }

        [JsonProperty("messageType")]
        public List<string> MessageType { get; set; }
    }


}
