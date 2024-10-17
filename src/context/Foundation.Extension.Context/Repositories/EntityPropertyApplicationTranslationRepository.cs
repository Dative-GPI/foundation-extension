using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Context.Repositories
{
    public class EntityPropertyApplicationTranslationRepository : IEntityPropertyApplicationTranslationRepository
    {
        private readonly DbSet<EntityPropertyApplicationTranslationDTO> _dbSet;

        public EntityPropertyApplicationTranslationRepository(BaseApplicationContext context)
        {
            _dbSet = context.EntityPropertyTranslations;
        }

        public async Task<IEnumerable<EntityPropertyApplicationTranslation>> GetMany(EntityPropertyApplicationTranslationsFilter filter)
        {
            var query = _dbSet
                .Include(e => e.EntityProperty)
                .AsQueryable();

            if (filter.ApplicationId.HasValue)
            {
                query = query.Where(dto => dto.ApplicationId == filter.ApplicationId);
            }

            if (filter.EntityPropertyId.HasValue)
            {
                query = query.Where(dto => dto.EntityPropertyId == filter.EntityPropertyId);
            }

            if (filter.EntityPropertiesIds != null)
            {
                query = query.Where(dto => filter.EntityPropertiesIds.Contains(dto.EntityPropertyId));
            }

            if (!string.IsNullOrEmpty(filter.EntityType))
            {
                query = query.Where(dto => dto.EntityProperty.EntityType == filter.EntityType);
            }

            var dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(dto => new EntityPropertyApplicationTranslation()
            {
                Id = dto.Id,
                Label = dto.Label,
                ApplicationId = dto.ApplicationId,
                EntityPropertyId = dto.EntityPropertyId,
				EntityPropertyCode = dto.EntityProperty.Code,
                LanguageCode = dto.LanguageCode
            });
        }

        public Task CreateRange(IEnumerable<CreateEntityPropertyApplicationTranslation> payload)
        {
            var dtos = payload.Select(e => new EntityPropertyApplicationTranslationDTO()
            {
                Id = Guid.NewGuid(),
                Label = e.Label,
                ApplicationId = e.ApplicationId,
                EntityPropertyId = e.EntityPropertyId,
                LanguageCode = e.LanguageCode,
                Disabled = false
            }).ToList();

            _dbSet.AddRange(dtos);

            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Guid> entityPropertiesIds)
        {
            var dtos = entityPropertiesIds.Select(id => new EntityPropertyApplicationTranslationDTO()
            {
                Id = id
            });
            _dbSet.RemoveRange(dtos);

            return Task.CompletedTask;
        }
    }
}