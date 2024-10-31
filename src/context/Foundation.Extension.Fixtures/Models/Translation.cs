using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Fixtures
{
    public class Translation : ICodeEntity
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string Context { get; set; }
    }
}