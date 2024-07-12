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
  public class UserOrganisationColumnRepository : IUserOrganisationColumnRepository
  {
    private DbSet<UserOrganisationColumnDTO> _dbSet;
    private BaseApplicationContext _context;

    public UserOrganisationColumnRepository(BaseApplicationContext context)
    {
      _dbSet = context.UserOrganisationColumns;
      _context = context;
    }

    public async Task<IEnumerable<UserOrganisationColumnInfos>> GetMany(UserOrganisationColumnsFilter filter)
    {
      var query = _dbSet
              .Include(uoc => uoc.Column)
              .AsQueryable();

      if (filter.UserOrganisationId.HasValue)
      {
        query = query.Where(uoc => uoc.UserOrganisationId == filter.UserOrganisationId.Value);
      }

      if (filter.TableId.HasValue)
      {
        query = query.Where(uoc => uoc.Column.TableId == filter.TableId.Value);
      }

      var dtos = await query.AsNoTracking().ToListAsync();

      return dtos.Select(userOrganisationColumnDTO => new UserOrganisationColumnInfos()
      {
        Id = userOrganisationColumnDTO.Id,
        ColumnId = userOrganisationColumnDTO.ColumnId,
        Hidden = userOrganisationColumnDTO.Hidden,
        Index = userOrganisationColumnDTO.Index,
        Disabled = userOrganisationColumnDTO.Disabled
      });
    }

    public Task Create(CreateUserOrganisationColumn payload)
    {
      var dto = new UserOrganisationColumnDTO()
      {
        Id = Guid.NewGuid(),
        UserOrganisationId = payload.UserOrganisationId,
        ColumnId = payload.ColumnId,
        Hidden = payload.Hidden,
        Index = payload.Index,
        Disabled = false
      };

      _dbSet.Add(dto);

      return Task.FromResult<IEntity<Guid>>(dto);
    }

    public Task CreateRange(IEnumerable<CreateUserOrganisationColumn> payload)
    {
      _dbSet.AddRange(
          payload.Select(p => new UserOrganisationColumnDTO()
          {
            Id = Guid.NewGuid(),
            ColumnId = p.ColumnId,
            UserOrganisationId = p.UserOrganisationId,
            Hidden = p.Hidden,
            Index = p.Index,
            Disabled = false
          })
      );

      return Task.CompletedTask;
    }

    public async Task Remove(Guid id)
    {
      var entity = await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
      _dbSet.Remove(entity);
    }

    public Task RemoveRange(IEnumerable<Guid> userOrganisationColumnsIds)
    {
      _dbSet.RemoveRange(userOrganisationColumnsIds.Select(id => new UserOrganisationColumnDTO()
      {
        Id = id
      }));

      return Task.CompletedTask;
    }
  }
}