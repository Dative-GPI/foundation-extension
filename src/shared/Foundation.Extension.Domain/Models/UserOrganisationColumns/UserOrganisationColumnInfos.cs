using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
	public class UserOrganisationColumnInfos
	{
		public Guid Id { get; set; }
		public Guid ColumnId { get; set; }
		public int Index { get; set; }
		public bool Hidden { get; set; }
		public bool Disabled { get; set; }
	}
}