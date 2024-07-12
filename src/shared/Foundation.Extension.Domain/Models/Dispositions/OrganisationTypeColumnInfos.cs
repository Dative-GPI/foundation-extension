using System;

namespace Foundation.Extension.Domain.Models
{
  public class OrganisationTypeColumnInfos
  {
    public Guid Id { get; set; }
    public Guid ColumnId { get; set; }
    public int Index { get; set; }
    public bool Hidden { get; set; }
    public bool Disabled { get; set; }
  }
}