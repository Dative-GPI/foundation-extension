using System;
using Bones.Flow;

namespace Foundation.Extension.Admin
{
    public interface IActorRequest : IRequest
    {
        Guid ActorId { get; }
    }
}