using System;
using Foundation.Extension.Gateway.Models;
using Foundation.Extension.Gateway.Abstractions;

namespace Foundation.Extension.Gateway.Providers
{
    public class RequestContextProvider : IRequestContextProvider
    {
        public RequestContext Context { get; set; }
    }
}