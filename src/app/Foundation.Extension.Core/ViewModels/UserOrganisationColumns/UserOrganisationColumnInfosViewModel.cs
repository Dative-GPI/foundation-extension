using System;

namespace Foundation.Extension.Core.ViewModels
{
  public class UserOrganisationColumnInfosViewModel
  {
    public Guid ColumnId { get; set; }
    public string Text => Label;
    public string Value { get; set; }
    public bool Sortable { get; set; }
    public bool Filterable { get; set; }
    public int Index { get; set; }
    public bool Hidden { get; set; }

    #region Translated properties
    public string Label { get; set; }
    #endregion
  }
}