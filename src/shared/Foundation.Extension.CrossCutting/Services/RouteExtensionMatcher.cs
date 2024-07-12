using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;

using Foundation.Extension.Domain.Abstractions;

namespace Foundation.Extension.CrossCutting.Services
{
  public class RouteExtensionMatcher : IExtensionMatcher
  {
    public bool TryMatch(string template, string value, out Dictionary<string, string> result, Dictionary<string, string> defaults = null)
    {
      var model = TemplateParser.Parse(template);
      var matcher = new TemplateMatcher(model, new RouteValueDictionary(defaults ?? new Dictionary<string, string>()));

      var values = new RouteValueDictionary();

      if (matcher.TryMatch(value, values))
      {
        result = values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        return true;
      }
      else
      {
        // URL does not match the template
        result = null;
        return false;
      }
    }
  }
}