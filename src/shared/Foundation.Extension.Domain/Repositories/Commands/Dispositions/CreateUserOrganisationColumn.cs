using System;
using System.Collections.Generic;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Repositories.Commands
{
	public class CreateUserOrganisationColumn
	{
		public required Guid UserOrganisationId { get; set; }
		public required Guid TableId { get; set; }
		public required Guid ColumnId { get; set; }
		public required int Index { get; set; }
		public required bool Hidden { get; set; }
	}
}