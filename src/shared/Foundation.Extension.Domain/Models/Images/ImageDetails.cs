using System;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Domain.Models
{
	public class ImageDetails
	{
		public Guid Id { get; set; }
		public string Label { get; set; }
		public string Path { get; set; }
		public string ThumbnailPath { get; set; }
		public string BlurHash { get; set; }
		public Scope Scope { get; set; }
		public Guid? ApplicationId { get; set; }
		public Guid? OrganisationId { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public Guid? UserId { get; set; }
	}
}