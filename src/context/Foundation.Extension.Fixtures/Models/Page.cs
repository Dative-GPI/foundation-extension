using System;
using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Fixtures
{
  public class Page : ICodeEntity
  {
    public string Code { get; set; }
    public string LabelDefault { get; set; }
    public bool ShowOnDrawer { get; set; }
  }
}