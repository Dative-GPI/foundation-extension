using Foundation.Extension.Gateway.Models;

namespace Foundation.Extension.Gateway.Abstractions
{
    public interface IRequestContextProvider
    {
        RequestContext Context { get; }
    }
}