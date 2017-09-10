using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            string rabbitMqAddress = "rabbitmq://localhost:5672";
            string virtualHost = "/";
            string rabbitMqQueue = "StayWell.SWIQ.CreateParticipant";

            Uri rabbitMqRootUri = new Uri(string.Concat(rabbitMqAddress, virtualHost));

            IBusControl rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
            {
                rabbit.Host(rabbitMqRootUri, settings =>
                {
                    settings.Password("guest");
                    settings.Username("guest");
                });
            });

            rabbitBusControl.Start();

            Task<ISendEndpoint> sendEndpointTask = rabbitBusControl.GetSendEndpoint(new Uri(string.Concat(rabbitMqAddress, virtualHost, rabbitMqQueue)));
            ISendEndpoint sendEndpoint = sendEndpointTask.Result;

            do
            {
                Console.WriteLine("Enter message type ('command', 'event', 'rpc') (or 'quit' to exit)");
                Console.Write("> ");
                string type = Console.ReadLine();

                if ("quit".Equals(type, StringComparison.OrdinalIgnoreCase))
                    break;

                Console.WriteLine("Enter data");
                Console.Write("> ");
                string data = Console.ReadLine();

                if (type.ToLower() == "command")
                {
                    sendEndpoint.Send<ICreateParticipant>(new
                    {
                        CreationDate = DateTime.UtcNow,
                        Data = data
                    });
                }
                else if (type.ToLower() == "event")
                {
                    rabbitBusControl.Publish<IParticipantUpdated>(new
                    {
                        DateUpdated = DateTime.UtcNow,
                        FirstName = data
                    });
                }
                else if (type.ToLower() == "rpc")
                {
                    IRequestClient<ICreateParticipant, ICreateParticipantResponse> client = 
                        rabbitBusControl.CreateRequestClient<ICreateParticipant, ICreateParticipantResponse>(new Uri(string.Concat(rabbitMqAddress, virtualHost, rabbitMqQueue)), TimeSpan.FromSeconds(10));

                    Task.Run(async () =>
                    {
                        ICreateParticipantResponse response = await client.Request(new CreateParticipant
                        {
                            CreationDate = DateTime.UtcNow,
                            Data = data
                        });

                        Console.WriteLine("Participant creation response: ParticipantId: {0}, Data: {1}", response.ParticipantId, response.Data);
                    }).Wait();
                }
                else
                {
                    Console.WriteLine("Wrong message type!");
                    Console.Write("> ");
                }
            }
            while (true);

            rabbitBusControl.Stop();
        }
    }
}
