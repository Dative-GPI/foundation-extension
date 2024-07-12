using System;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Tools
{
    public class RequestContextProvider : IRequestContextProvider
    {
        public RequestContext Context { get; set; }
    }
}