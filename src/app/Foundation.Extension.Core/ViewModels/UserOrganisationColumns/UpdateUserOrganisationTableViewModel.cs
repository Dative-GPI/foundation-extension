using System;
using System.Collections.Generic;

namespace Foundation.Extension.Core.ViewModels
{
	public class UpdateUserOrganisationTableViewModel
	{
		public string TableCode { get; set; }
		public string Mode { get; set; }
		public string SortBy { get; set; }
		public string SortOrder { get; set; }
		public int RowsPerPage { get; set; }

		public List<UpdateUserOrganisationColumnViewModel> Columns { get; set; }
	}

	public class UpdateUserOrganisationColumnViewModel
	{
		public Guid ColumnId { get; set; }
		public int Index { get; set; }
		public bool Hidden { get; set; }
		public bool Sortable { get; set; }
		public bool Filterable { get; set; }
	}
}