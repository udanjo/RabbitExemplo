using MassTransit.Api.Consumer;
using MassTransit.Api.Producer;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Api.DependencyInjections
{
    public static class RabbitMqConfiguration
    {
        public static IServiceCollection ConfigurationRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(r =>
            {
                r.AddConsumer<ReceiverConsumer, ReceiverConsumerDefinition>();

                ConfigurationRabbitMqHost(r);
            });

            services.AddScoped(typeof(IRabbitMqProducer<>), typeof(RabbitMqProducer<>));

            return services;
        }

        private static void ConfigurationRabbitMqHost(IBusRegistrationConfigurator registration)
        {
            registration.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", 5672, "test", h =>
                {
                    h.Username("admin");
                    h.Password("admin");
                });

                cfg.ConfigureEndpoints(context);
            });
        }
    }
}