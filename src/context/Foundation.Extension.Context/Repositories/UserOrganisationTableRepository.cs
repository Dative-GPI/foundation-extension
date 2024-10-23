using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Bones.Repository.Interfaces;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Context.Repositories
{
	public class UserOrganisationTableRepository : IUserOrganisationTableRepository
	{
		private DbSet<UserOrganisationTableDTO> _dbSet;
		private BaseApplicationContext _context;

		public UserOrganisationTableRepository(BaseApplicationContext context)
		{
			_dbSet = context.UserOrganisationTables;
			_context = context;
		}


		public async Task<UserOrganisationTableDetails> Get(Guid userOrganisationTableId)
		{
			var dto = await _dbSet
				.Include(uot => uot.Table)
				.AsNoTracking()
				.SingleOrDefaultAsync(uot => uot.Id == userOrganisationTableId);

			if (dto == default)
			{
				return null;
			}

			return new UserOrganisationTableDetails()
			{
				Id = dto.Id,
				Code = dto.Table.Code,
				Mode = dto.Mode,
				RowsPerPage = dto.RowsPerPage,
				SortByKey = dto.SortByKey,
				SortByOrder = dto.SortByOrder
			};
		}

		public async Task<UserOrganisationTableDetails> Find(string tableCode, Guid userOrganisationId)
		{
			var dto = await _dbSet
				.Include(uot => uot.Table)
				.AsNoTracking()
				.SingleOrDefaultAsync(uot => uot.UserOrganisationId == userOrganisationId && uot.Table.Code == tableCode);

			if (dto == default)
			{
				return null;
			}

			return new UserOrganisationTableDetails()
			{
				Id = dto.Id,
				Code = dto.Table.Code,
				Mode = dto.Mode,
				RowsPerPage = dto.RowsPerPage,
				SortByKey = dto.SortByKey,
				SortByOrder = dto.SortByOrder
			};
		}


		public Task<IEntity<Guid>> Create(CreateUserOrganisationTable payload)
		{
			var dto = new UserOrganisationTableDTO()
			{
				Id = Guid.NewGuid(),
				UserOrganisationId = payload.UserOrganisationId,
				TableId = payload.TableId,
				Mode = payload.Mode,
				RowsPerPage = payload.RowsPerPage,
				SortByKey = payload.SortByKey,
				SortByOrder = payload.SortByOrder,
				Disabled = false
			};

			var result = _dbSet.Add(dto);

			return Task.FromResult<IEntity<Guid>>(result.Entity);
		}

		public async Task<IEntity<Guid>> Update(UpdateUserOrganisationTable payload)
		{
			var dto = await _dbSet
				.AsNoTracking()
				.SingleOrDefaultAsync(a => a.Id == payload.Id);

			if (dto == default)
			{
				return null;
			}

			dto.Mode = payload.Mode;
			dto.SortByKey = payload.SortByKey;
			dto.SortByOrder = payload.SortByOrder;
			dto.RowsPerPage = payload.RowsPerPage;

			return _dbSet.Update(dto).Entity;
		}


		public async Task Remove(Guid id)
		{
			var entity = await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
			_dbSet.Remove(entity);
		}
	}
}