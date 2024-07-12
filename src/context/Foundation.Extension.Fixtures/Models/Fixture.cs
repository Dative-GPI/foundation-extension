using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Fixtures
{
    public class Fixture : ICodeEntity
    {
        public string Code { get; set; }
        public string Value { get; set; }
    }
}