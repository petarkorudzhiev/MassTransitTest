using Contracts;
using System;

namespace Subscriber
{
    public class CreateParticipantResponse : ICreateParticipantResponse
    {
        public Guid ParticipantId { get; set; }
        public string Data { get; set; }
    }
}
