using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Abstractions
{
    public interface ITreeItem<T> where T : ITreeItem<T>
    {
        Guid Id { get; set; }
        Guid? ParentId { get; set; }
		List<T> Children { get; set; }
	}
}