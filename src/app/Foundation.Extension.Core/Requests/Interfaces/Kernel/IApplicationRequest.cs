using System;
using Bones.Flow;

namespace Foundation.Extension.Core
{
    public interface IApplicationRequest : IRequest
    {
        Guid ApplicationId { get; }
    }
}