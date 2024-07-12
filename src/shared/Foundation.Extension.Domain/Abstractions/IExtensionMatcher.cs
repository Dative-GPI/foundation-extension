using System.Collections.Generic;

namespace Foundation.Extension.Domain.Abstractions
{
    public interface IExtensionMatcher
    {
        bool TryMatch(string template, string value, out Dictionary<string, string> result, Dictionary<string, string> defaults = null);
    }
}