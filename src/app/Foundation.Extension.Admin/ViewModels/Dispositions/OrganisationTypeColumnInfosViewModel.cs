using System;
using System.Collections.Generic;

namespace Foundation.Extension.Admin.ViewModels
{
  public class OrganisationTypeColumnInfosViewModel
  {
    public Guid Id { get; set; }
    public Guid ColumnId { get; set; }
    public int Index { get; set; }
    public bool Hidden { get; set; }
    public bool Disabled { get; set; }
  }
}