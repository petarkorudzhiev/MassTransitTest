using System;

namespace Contracts
{
    public interface ICreateParticipantResponse
    {
        Guid ParticipantId { get; set; }
        string Data { get; set; }
    }
}
