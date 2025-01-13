using System.Collections.Generic;

namespace Foundation.Extension.Domain.Abstractions
{
    public interface ITemplateMatcher
    {
        bool TryMatch(string template, string value, out Dictionary<string, string> result, Dictionary<string, string> defaults = null);
    }
}