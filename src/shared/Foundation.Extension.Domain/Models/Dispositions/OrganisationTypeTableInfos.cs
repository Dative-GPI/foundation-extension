using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
  public class OrganisationTypeTableInfos
  {
    public Guid OrganisationTypeId { get; set; }
    public Guid TableId { get; set; }
    public List<OrganisationTypeColumnInfos> Dispositions { get; set; }
  }
}