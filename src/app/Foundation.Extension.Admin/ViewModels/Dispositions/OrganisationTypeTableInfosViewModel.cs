using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation.Extension.Admin.ViewModels
{
	public class OrganisationTypeTableInfosViewModel
	{
		public Guid OrganisationTypeId { get; set; }
		public Guid TableId { get; set; }
		public List<OrganisationTypeColumnInfosViewModel> Dispositions { get; set; }
	}
}