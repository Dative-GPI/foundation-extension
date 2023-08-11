using System;

using Foundation.Template.Admin.Models;
using Foundation.Template.Admin.Abstractions;

namespace Foundation.Template.Admin.Providers
{
    public class RequestContextProvider : IRequestContextProvider
    {
        public RequestContext Context { get; set; }
    }
}