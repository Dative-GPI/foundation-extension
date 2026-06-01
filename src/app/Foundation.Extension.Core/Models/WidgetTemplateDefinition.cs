using System.Collections.Generic;

namespace Foundation.Extension.Core.Models
{
    public class WidgetTemplateDefinition
    {
        public string Code { get; set; } = string.Empty;
        public IEnumerable<string> Authorizations { get; set; } = new List<string>();
    }
}
