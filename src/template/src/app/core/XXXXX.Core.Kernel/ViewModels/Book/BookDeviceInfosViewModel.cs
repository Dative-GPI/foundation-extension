using System;
using Foundation.Clients.Gateway.FoundationModels;
using Foundation.Extension.Domain.Enums;
using Foundation.Extension.Domain.Attributes;

namespace XXXXX.Core.Kernel.ViewModels
{
    [EntityDescription("BookDevice", EntityKind.Infos)]
	public class BookDeviceInfosViewModel : LanguageInfosFoundationModel
	{
		[CustomProperty("Category", "The category of the book")]
		public string Category { get; set; }
		[CustomProperty("Value", "The value of the book device")]
		public string Value { get; set; }
	}
}