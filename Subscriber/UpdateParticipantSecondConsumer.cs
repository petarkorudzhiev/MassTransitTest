using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public class UpdateParticipantSecondConsumer : IConsumer<IParticipantUpdated>
    {
        public async Task Consume(ConsumeContext<IParticipantUpdated> context)
        {
            await Console.Out.WriteLineAsync($"Second consumer Updating participant -> Date updated: {context.Message.DateUpdated}, FirstName: {context.Message.FirstName}");
        }
    }
}
