using System;

namespace Foundation.Extension.Domain.Abstractions
{
    public interface ITreeItem
    {
        Guid Id { get; set; }
        Guid? ParentId { get; set; }
		string Label { get; set; }
	}
}