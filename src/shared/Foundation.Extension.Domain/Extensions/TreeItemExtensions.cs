using System;
using System.Linq;
using System.Collections.Generic;

using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Extensions
{
	public static class TreeItemExtensions
	{
		public static (bool, List<PathCrumb>) GetPath<T>(Guid? folderId, Dictionary<Guid, T> dictionary, List<PathCrumb> result) where T : ITreeItem
        {
            if (!folderId.HasValue || !dictionary.TryGetValue(folderId.Value, out var parent))
            {
                return (true, result);
            }

            if (result.Any(p => p.Id == parent.Id))
            {
                return (false, result);
            }

            result.Add(new PathCrumb()
            {
                Id = parent.Id,
                Index = result.Count,
                Label = parent.Label
            });

            return GetPath(parent.ParentId, dictionary, result);
        }
	}
}