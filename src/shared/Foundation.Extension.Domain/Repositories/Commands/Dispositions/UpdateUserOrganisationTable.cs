using System;

namespace Foundation.Extension.Domain.Repositories.Commands
{
  public class UpdateUserOrganisationTable
  {
    public required Guid Id { get; set; }
    public string Mode { get; set; }
    public string SortByKey { get; set; }
    public string SortByOrder { get; set; }
    public int RowsPerPage { get; set; }
    public required bool Disabled { get; set; }
  }
}