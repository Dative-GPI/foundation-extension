using System.Collections.Generic;
using Bones.Flow;

namespace Foundation.Extension.Core
{
    public interface IAuthorizedRequest : IRequest
    {
        IEnumerable<string> Authorizations { get; }
    }
}