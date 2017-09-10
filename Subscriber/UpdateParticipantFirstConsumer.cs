using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public class UpdateParticipantFirstConsumer : IConsumer<IParticipantUpdated>
    {
        public async Task Consume(ConsumeContext<IParticipantUpdated> context)
        {
            await Console.Out.WriteLineAsync($"First consumer Updating participant -> Date updated: {context.Message.DateUpdated}, FirstName: {context.Message.FirstName}");
        }
    }
}
