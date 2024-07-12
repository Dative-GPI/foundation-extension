using System.Collections.Generic;
using Bones.Flow;

namespace Foundation.Extension.Admin
{
    public interface IAuthorizedRequest : IRequest
    {
        IEnumerable<string> Authorizations { get; }
    }
}