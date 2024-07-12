using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Context;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Context.Repositories
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly DbSet<ColumnDTO> _dbSet;

        public ColumnRepository(BaseApplicationContext context)
        {
            _dbSet = context.Columns;
        }

        public async Task<IEnumerable<Column>> GetMany(ColumnsFilter filter)
        {
            var query = _dbSet
                .Include(c => c.EntityProperty)
                .AsQueryable();

            query = query.Where(s => s.ApplicationId == filter.ApplicationId);

            if (filter.TableId.HasValue)
            {
                query = query.Where(s => s.TableId == filter.TableId.Value);
            }

            var dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(columnDTO => new Column()
            {
                Id = columnDTO.Id,
                TableId = columnDTO.TableId,
                PropertyType = columnDTO.PropertyType,
                EntityPropertyId = columnDTO.EntityPropertyId,
                Code = columnDTO.PropertyType switch
                {
                    PropertyType.EntityProperty => columnDTO.EntityProperty.Code,
                    _ => null
                },
                Value = columnDTO.PropertyType switch
                {
                    PropertyType.EntityProperty => columnDTO.EntityProperty.Value,
                    _ => null
                },
                Label = columnDTO.PropertyType switch
                {
                    PropertyType.EntityProperty => columnDTO.EntityProperty.LabelDefault,
                    _ => null
                },
                Index = columnDTO.Index,
                Filterable = columnDTO.Filterable,
                Sortable = columnDTO.Sortable,
                Hidden = columnDTO.Hidden,
                Disabled = columnDTO.Disabled
            }).OrderBy(c => c.Index).ToList();
        }

        public Task<IEntity<Guid>> Create(CreateColumn payload)
        {
            var columnDTO = _dbSet.Add(new ColumnDTO()
            {
                Id = Guid.NewGuid(),
                ApplicationId = payload.ApplicationId,
                TableId = payload.TableId,
                EntityPropertyId = payload.EntityPropertyId,
                Index = payload.Index,
                PropertyType = payload.PropertyType,
                Filterable = payload.Filterable,
                Sortable = payload.Sortable,
                Hidden = payload.Hidden,
                Disabled = payload.Disabled
            });

            return Task.FromResult<IEntity<Guid>>(columnDTO.Entity);
        }

        public async Task CreateRange(IEnumerable<CreateColumn> payload)
        {
            foreach (var createColumn in payload)
            {
                await Create(createColumn);
            }
        }

        public async Task<IEntity<Guid>> Update(UpdateColumn payload)
        {
            var columnDTO = await _dbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == payload.Id);

            if (columnDTO == default)
            {
                return null;
            }

            columnDTO.Index = payload.Index;
            columnDTO.Filterable = payload.Filterable;
            columnDTO.Sortable = payload.Sortable;
            columnDTO.Hidden = payload.Hidden;
            columnDTO.Disabled = payload.Disabled;

            return _dbSet.Update(columnDTO).Entity;
        }

        public async Task UpdateRange(IEnumerable<UpdateColumn> payload)
        {
            var updateIds = payload.Select(p => p.Id).ToList();

            var columnDTOs = await _dbSet
                .Where(c => updateIds.Contains(c.Id))
                .AsNoTracking()
                .ToListAsync();
            
            var updateColumns = payload.ToDictionary(p => p.Id);

            foreach (var columnDTO in columnDTOs)
            {
                if (updateColumns.TryGetValue(columnDTO.Id, out var update))
                {
                    columnDTO.Index = update.Index;
                    columnDTO.Filterable = update.Filterable;
                    columnDTO.Sortable = update.Sortable;
                    columnDTO.Hidden = update.Hidden;
                    columnDTO.Disabled = update.Disabled;

                    _dbSet.Update(columnDTO);
                }
            }
        }
    }
}