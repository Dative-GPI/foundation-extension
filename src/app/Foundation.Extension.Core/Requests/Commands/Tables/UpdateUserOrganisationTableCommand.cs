using System;
using System.Collections.Generic;
using System.Linq;
using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
	public class UpdateUserOrganisationTableCommand : IRequest, ICoreRequest
	{
		public IEnumerable<string> Authorizations => Enumerable.Empty<string>();

		public string TableCode { get; set; }
		public string Mode { get; set; }
		public string SortByKey { get; set; }
		public string SortByOrder { get; set; }
		public int RowsPerPage { get; set; }
		public IEnumerable<UpdateUserOrganisationColumnCommand> Columns { get; set; }
	}

	public class UpdateUserOrganisationColumnCommand
	{
		public int Index { get; set; }
		public bool Hidden { get; set; }
		public bool Sortable { get; set; }
		public bool Filterable { get; set; }
		public Guid ColumnId { get; set; }
	}
}