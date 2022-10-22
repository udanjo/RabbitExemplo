namespace MassTransit.Publish.Consumer
{
    public class ReceiverConsumerDefinition : ConsumerDefinition<ReceiverConsumer>
    {
        public ReceiverConsumerDefinition()
        {
            EndpointName = "notificationmessage";
            ConcurrentMessageLimit = 1;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ReceiverConsumer> consumerConfigurator)
        {
            //Define a quantidade de retry por tempo de tentativa
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 450));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}