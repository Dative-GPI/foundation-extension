

using System;
using Bones.Flow;

namespace Foundation.Extension.Gateway.Requests.Commands
{
    public class RemoveApplicationCommand : IRequest
    {
        public Guid ApplicationId { get; set; }
    }
}