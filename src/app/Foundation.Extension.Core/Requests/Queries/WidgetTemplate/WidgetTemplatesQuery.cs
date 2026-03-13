using System;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
    public class WidgetTemplatesQuery : IRequest<IEnumerable<WidgetTemplateInfos>>, ICoreRequest
    {
        public IEnumerable<string> Authorizations { get; set; } = new List<string>();

        public string Search { get; set; }
    }
}