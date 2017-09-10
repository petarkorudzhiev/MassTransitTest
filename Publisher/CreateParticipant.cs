using Contracts;
using System;

namespace Publisher
{
    public class CreateParticipant : ICreateParticipant
    {
        public DateTime CreationDate { get; set; }
        public string Data { get; set; }
    }
}
