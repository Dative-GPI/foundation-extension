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
  public class OrganisationTypeDispositionRepository : IOrganisationTypeDispositionRepository
  {
    private DbSet<OrganisationTypeDispositionDTO> _dbSet;
    private BaseApplicationContext _context;

    public OrganisationTypeDispositionRepository(BaseApplicationContext context)
    {
      _dbSet = context.OrganisationTypeDispositions;
      _context = context;
    }

    public Task Create(CreateOrganisationTypeDisposition payload)
    {
      var dto = new OrganisationTypeDispositionDTO()
      {
        Id = Guid.NewGuid(),
        ColumnId = payload.ColumnId,
        Hidden = payload.Hidden,
        Disabled = payload.Disabled,
        Index = payload.Index,
        OrganisationTypeId = payload.OrganisationTypeId
      };

      _dbSet.Add(dto);

      return Task.FromResult<IEntity<Guid>>(dto);
    }

    public Task CreateMany(IEnumerable<CreateOrganisationTypeDisposition> payload)
    {
      _dbSet.AddRange(
          payload.Select(p => new OrganisationTypeDispositionDTO()
          {
            ColumnId = p.ColumnId,
            Disabled = p.Disabled,
            Hidden = p.Hidden,
            Id = Guid.NewGuid(),
            Index = p.Index,
            OrganisationTypeId = p.OrganisationTypeId
          })
      );

      return Task.CompletedTask;
    }

    public async Task<IEnumerable<OrganisationTypeColumnInfos>> GetMany(ColumnOrganisationTypesFilter filter)
    {
      IQueryable<OrganisationTypeDispositionDTO> set = _dbSet;

      if (filter.OrganisationTypeId.HasValue)
      {
        set = set.Where(s => s.OrganisationTypeId == filter.OrganisationTypeId.Value);
      }

      if (filter.TableId.HasValue)
      {
        set = set.Where(s => s.Column.TableId == filter.TableId.Value);
      }

      var result = await set.AsNoTracking().ToListAsync();

      return result.Select(r => new OrganisationTypeColumnInfos()
      {
        Id = r.Id,
        ColumnId = r.ColumnId,
        Disabled = r.Disabled,
        Hidden = r.Hidden,
        Index = r.Index
      });
    }

    public async Task Remove(Guid id)
    {
      var entity = await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
      _dbSet.Remove(entity);
    }

    public async Task RemoveMany(IEnumerable<Guid> ids)
    {
      var entities = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
      _dbSet.RemoveRange(entities);
    }
  }
}