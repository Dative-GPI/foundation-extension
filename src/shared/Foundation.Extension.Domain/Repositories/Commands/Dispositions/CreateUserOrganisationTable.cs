using System;

namespace Foundation.Extension.Domain.Repositories.Commands
{
	public class CreateUserOrganisationTable
	{
		public required Guid TableId { get; set; }
		public required Guid UserOrganisationId { get; set; }
		public string Mode { get; set; }
		public string SortByKey { get; set; }
		public string SortByOrder { get; set; }
		public int RowsPerPage { get; set; }
	}
}