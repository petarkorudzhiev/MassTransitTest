using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public class CreateParticipantConsumer : IConsumer<ICreateParticipant>
    {
        public async Task Consume(ConsumeContext<ICreateParticipant> context)
        {
            await Console.Out.WriteLineAsync($"Creating participant -> Creation date: {context.Message.CreationDate}, Data: {context.Message.Data}");


            // Used only in RPC/(Request - Response) pattern
            context.Respond(new CreateParticipantResponse
            {
                ParticipantId = Guid.NewGuid(),
                Data = context.Message.Data
            });
        }
    }
}
