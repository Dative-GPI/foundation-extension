using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Shared;

namespace XXXXX.Core.Kernel.Models
{
    public class ActionDefinition
    {
        public IEnumerable<string> Authorizations { get; set; }
        public string LabelDefault { get; set; }
        public string LabelCode { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string RouteTemplate { get; set; }
        public Func<Dictionary<string, string>, IServiceProvider, Task<string>> ComputePath { get; set; }
        public ActionType ActionType { get; set; }
        public string Uri { get; set; }
    }
}
