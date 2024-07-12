using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
  public class UserOrganisationTableDetails
  {
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Mode { get; set; }
    public int RowsPerPage { get; set; }
    public string SortByKey { get; set; }
    public string SortByOrder { get; set; }
    public IEnumerable<CompleteUserOrganisationColumnInfos> Columns { get; set; }
  }
}