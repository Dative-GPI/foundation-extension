using System;

namespace Foundation.Extension.Domain.Models
{
	public class Table
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Label { get; set; }
		public string EntityType { get; set; }
	}
}