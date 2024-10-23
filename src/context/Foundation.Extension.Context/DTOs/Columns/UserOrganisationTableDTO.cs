using System;

using Bones.Repository.Interfaces;

namespace Foundation.Extension.Context.DTOs
{
	public class UserOrganisationTableDTO : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public Guid TableId { get; set; }
		public TableDTO Table { get; set; }
		public Guid UserOrganisationId { get; set; }
		public string Mode { get; set; }
		public int RowsPerPage { get; set; }
		public string SortByKey { get; set; }
		public string SortByOrder { get; set; }
		public bool Disabled { get; set; }
	}
}
