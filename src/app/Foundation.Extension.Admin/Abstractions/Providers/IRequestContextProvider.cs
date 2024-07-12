using Foundation.Extension.Admin.Models;

namespace Foundation.Extension.Admin.Abstractions
{
    public interface IRequestContextProvider
    {
        RequestContext Context { get; }
    }
}