using System;

using Foundation.Extension.Admin.Models;
using Foundation.Extension.Admin.Abstractions;

namespace Foundation.Extension.Admin.Providers
{
    public class RequestContextProvider : IRequestContextProvider
    {
        public RequestContext Context { get; set; }
    }
}