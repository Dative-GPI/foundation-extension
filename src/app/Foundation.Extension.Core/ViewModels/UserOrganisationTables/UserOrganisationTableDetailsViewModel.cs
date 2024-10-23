using System;
using System.Collections.Generic;

namespace Foundation.Extension.Core.ViewModels
{
	public class UserOrganisationTableDetailsViewModel
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Mode { get; set; }
		public int RowsPerPage { get; set; }
		public string SortBy { get; set; }
		public string SortOrder { get; set; }

		public List<UserOrganisationColumnInfosViewModel> Columns { get; set; }
	}
}