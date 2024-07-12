

using System;
using Bones.Flow;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Gateway.Requests.Commands
{
    public class CreateApplicationCommand : IRequest<IEntity<Guid>>
    {
        public Guid ApplicationId { get; set; }
        public string Host { get; set; }
        public string AdminJWT { get; set; }
    }
}