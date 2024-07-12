using System;
using Bones.Flow;

namespace Foundation.Extension.Core
{
    public interface IActorRequest : IRequest
    {
        Guid ActorId { get; }
    }
}