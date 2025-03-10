using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class WidgetTemplateQuery : IRequest<WidgetTemplateDetails>, ICoreRequest
    {
        public IEnumerable<string> Authorizations => new List<string>();
        
        public Guid WidgetTemplateId { get; set; }
    }
}