using System;
using Foundation.Clients.Gateway.FoundationModels;
using Foundation.Extension.Domain.Enums;
using Foundation.Extension.Domain.Attributes;

namespace XXXXX.Core.Kernel.ViewModels
{
    [EntityDescription("BookDevice", EntityKind.Infos)]
	public class BookDeviceInfosViewModel : LanguageInfosFoundationModel
	{
		[CustomProperty("Category")]
		public string Category { get; set; }
		[CustomProperty("Value")]
		public string Value { get; set; }
	}
}