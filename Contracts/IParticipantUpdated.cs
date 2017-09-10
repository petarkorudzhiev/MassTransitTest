using System;

namespace Contracts
{
    public interface IParticipantUpdated
    {
        DateTime DateUpdated { get; set; }
        string FirstName { get; set; }
    }
}
