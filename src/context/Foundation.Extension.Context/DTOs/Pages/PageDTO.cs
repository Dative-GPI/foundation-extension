using System;

using Bones.Repository.Interfaces;
using Foundation.Extension.Context.Abstractions;

namespace Foundation.Extension.Context.DTOs
{
  public class PageDTO : IEntity<Guid>, ICodeEntity
  {
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string LabelDefault { get; set; }
    public bool ShowOnDrawer { get; set; }
    public bool Disabled { get; set; }
  }
}