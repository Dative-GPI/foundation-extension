using System;
using System.Linq;
using System.Collections.Generic;

using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Extensions
{
	public static class TreeItemExtensions
	{
		public static (bool, List<PathCrumb>) GetPath<T>(Guid? folderId, Dictionary<Guid, T> dictionary, Func<T, string> label, List<PathCrumb> result) where T : ITreeItem<T>
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
				Label = label(parent)
			});

			return GetPath(parent.ParentId, dictionary, label, result);
		}

		public static List<Guid> GetRecursiveItemIds<T>(Guid parentId, Dictionary<Guid, T> dictionary, Func<T, List<Guid>> getItemIds, List<Guid> itemIds) where T : ITreeItem<T>
		{
			if (dictionary.TryGetValue(parentId, out var current))
			{
				itemIds.AddRange(getItemIds(current));

				if (current.Children != null && current.Children.Count > 0)
				{
					foreach (var child in current.Children)
					{
						itemIds = GetRecursiveItemIds(child.Id, dictionary, getItemIds, itemIds);
					}
				}
			}

			return itemIds.Distinct().ToList();
		}
	}
}