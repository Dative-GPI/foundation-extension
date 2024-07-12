using System;

using Bones.Repository.Interfaces;

namespace Foundation.Extension.Context.DTOs
{
  public class UserOrganisationColumnDTO : IEntity<Guid>
  {
    public Guid Id { get; set; }
    public Guid ColumnId { get; set; }
    public ColumnDTO Column { get; set; }
    public Guid UserOrganisationId { get; set; }

    public bool Hidden { get; set; }
    public int Index { get; set; }
    public bool Disabled { get; set; }
  }
}