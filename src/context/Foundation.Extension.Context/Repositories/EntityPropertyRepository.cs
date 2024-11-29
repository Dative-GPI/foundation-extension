using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Bones.Repository.Interfaces;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Context;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Context.Repositories
{
    public class EntityPropertyRepository : IEntityPropertyRepository
    {
        private readonly DbSet<EntityPropertyDTO> _dbSet;

        public EntityPropertyRepository(BaseApplicationContext context)
        {
            _dbSet = context.EntityProperties;
        }

        public async Task<EntityProperty> Get(Guid id)
        {
            var dto = await _dbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(ep => ep.Id == id);

            if (dto == null) return null;

            return new EntityProperty()
            {
                Id = dto.Id,
                Code = dto.Code,
                EntityType = dto.EntityType,
                ParentId = dto.ParentId,
                Value = dto.Value,
				EntityKind = Enum.Parse<EntityKind>(dto.EntityKind),
				TranslationCode = dto.TranslationCode,
                Disabled = dto.Disabled
            };
        }

        public async Task<IEnumerable<EntityProperty>> GetMany(EntityPropertiesFilter filter)
        {
            var query = _dbSet
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(filter.EntityType))
            {
                query = query.Where(ep => ep.EntityType == filter.EntityType);
            }

            if(filter.EntityPropertiesIds != null)
            {
                query = query.Where(ep => filter.EntityPropertiesIds.Contains(ep.Id));
            }

            var dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(dto => new EntityProperty()
            {
                Id = dto.Id,
                Code = dto.Code,
                EntityType = dto.EntityType,
                ParentId = dto.ParentId,
                Value = dto.Value,
				EntityKind = Enum.Parse<EntityKind>(dto.EntityKind),
				TranslationCode = dto.TranslationCode,
                Disabled = dto.Disabled
            }).OrderBy(e => e.Code).ToList();
        }
    }
}