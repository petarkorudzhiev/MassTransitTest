using System;

namespace Contracts
{
    public interface ICreateParticipant
    {
        DateTime CreationDate { get; set; }
        string Data { get; set; }
    }
}
