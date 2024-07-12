using System.Threading.Tasks;

using Foundation.Extension.Core.Models;

namespace Foundation.Extension.Core.Abstractions
{
    public interface IRequestContextProvider
    {
        RequestContext Context { get; }
    }
}