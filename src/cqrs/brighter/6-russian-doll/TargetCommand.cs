using System;
using Paramore.Brighter;

namespace RussianDoll
{
    public class TargetCommand : IRequest
    {
        public TargetCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}