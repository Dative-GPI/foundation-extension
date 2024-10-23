using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Context.Repositories
{
	public class PageRepository : IPageRepository
	{
		private readonly DbSet<PageDTO> _dbSet;

		public PageRepository(BaseApplicationContext context)
		{
			_dbSet = context.Pages;
		}

		public async Task<IEnumerable<Page>> GetMany(PagesFilter filter)
		{
			var query = _dbSet
				.AsQueryable();

			if (filter.ShowOnDrawer.HasValue)
			{
				query = query.Where(p => p.ShowOnDrawer == filter.ShowOnDrawer.Value);
			}

			var dtos = await query.AsNoTracking().ToListAsync();

			return dtos.Select(dto => new Page()
			{
				Id = dto.Id,
				Code = dto.Code,
				LabelDefault = dto.LabelDefault
			}).ToList();
		}
	}
}