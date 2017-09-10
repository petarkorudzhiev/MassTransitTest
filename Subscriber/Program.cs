using MassTransit;
using System;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            string rabbitMqAddress = "rabbitmq://localhost:5672";
            string virtualHost = "/";
            string rabbitMqQueue = "StayWell.SWIQ.CreateParticipant";

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(string.Concat(rabbitMqAddress, virtualHost)), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, rabbitMqQueue, e =>
                {
                    e.Consumer<CreateParticipantConsumer>();
                    e.Consumer<UpdateParticipantFirstConsumer>();
                    e.Consumer<UpdateParticipantSecondConsumer>();
                });
            });

            busControl.Start();
            Console.Read();
            busControl.Stop();
        }
    }
}
