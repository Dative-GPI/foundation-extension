using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Context.Repositories
{
  public class TableRepository : ITableRepository
  {
    private BaseApplicationContext _context;
    private DbSet<TableDTO> _dbSet;

    public TableRepository(BaseApplicationContext context)
    {
      _context = context;
      _dbSet = context.Tables;
    }

    public async Task<Table> Get(Guid id)
    {
      var dto = await _dbSet.SingleOrDefaultAsync(t => t.Id == id);

      return new Table()
      {
        Code = dto.Code,
        EntityType = dto.EntityType,
        Id = dto.Id,
        Label = dto.LabelDefault
      };
    }

    public async Task<Table> GetFromCode(string code)
    {
      var dto = await _dbSet.SingleOrDefaultAsync(t => t.Code == code);

      if (dto == default)
      {
        return null;
      }

      return new Table()
      {
        Code = dto.Code,
        EntityType = dto.EntityType,
        Id = dto.Id,
        Label = dto.LabelDefault
      };
    }

    public async Task<IEnumerable<Table>> GetMany(TablesFilter filter)
    {
      IQueryable<TableDTO> set = _dbSet;

      var result = await set.ToListAsync();

      return result.Select(s => new Table()
      {
        Code = s.Code,
        EntityType = s.EntityType,
        Id = s.Id,
        Label = s.LabelDefault
      });
    }
  }
}