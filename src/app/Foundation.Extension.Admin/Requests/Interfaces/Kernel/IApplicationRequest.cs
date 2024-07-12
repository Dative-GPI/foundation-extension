using System;
using Bones.Flow;

namespace Foundation.Extension.Admin
{
    public interface IApplicationRequest : IRequest
    {
        Guid ApplicationId { get; }
    }
}