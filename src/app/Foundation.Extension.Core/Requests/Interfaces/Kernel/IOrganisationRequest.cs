using System;
using Bones.Flow;

namespace Foundation.Extension.Core
{
    public interface IOrganisationRequest : IRequest
    {
        Guid OrganisationId { get; }
    }
}