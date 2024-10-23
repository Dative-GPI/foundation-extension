using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
	public class CompleteUserOrganisationColumnInfos
	{
		public Guid ColumnId { get; set; }
		public int Index { get; set; }
		public bool Hidden { get; set; }
		public string Value { get; set; }
		public bool Sortable { get; set; }
		public bool Filterable { get; set; }

		#region Translated properties
		public string Label { get; set; }
		#endregion

	}
}
