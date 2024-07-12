using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Bones.Repository.Interfaces;

using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Abstractions;

namespace Foundation.Extension.Context.Repositories
{
    public class EntityPropertyTranslationRepository : IEntityPropertyTranslationRepository
    {
        private readonly DbSet<EntityPropertyTranslationDTO> _dbSet;
        private readonly IFoundationClientFactory _foundationClientFactory;

        public EntityPropertyTranslationRepository(BaseApplicationContext context)
        {
            _dbSet = context.EntityPropertyTranslations;
        }

        public async Task<IEnumerable<EntityPropertyTranslation>> GetMany(EntityPropertyTranslationsFilter filter)
        {
            var query = _dbSet
                .AsQueryable();

            if (filter.ApplicationId.HasValue)
            {
                query = query.Where(dto => dto.ApplicationId == filter.ApplicationId);
            }

            if (filter.EntityPropertyId.HasValue)
            {
                query = query.Where(dto => dto.EntityPropertyId == filter.EntityPropertyId);
            }

            if (!string.IsNullOrEmpty(filter.EntityType))
            {
                query = query.Where(dto => dto.EntityProperty.EntityType == filter.EntityType);
            }


            var dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(dto => new EntityPropertyTranslation()
            {
                Id = dto.Id,
                Label = dto.Label,
                ApplicationId = dto.ApplicationId,
                EntityPropertyId = dto.EntityPropertyId,
                CategoryLabel = dto.CategoryLabel,
                LanguageCode = dto.LanguageCode,
            });
        }

        public Task CreateRange(IEnumerable<CreateEntityPropertyTranslation> payload)
        {

            var dtos = payload.Select(e => new EntityPropertyTranslationDTO()
            {
                Id = Guid.NewGuid(),
                Label = e.Label,
                ApplicationId = e.ApplicationId,
                EntityPropertyId = e.EntityPropertyId,
                CategoryLabel = e.CategoryLabel,
                LanguageCode = e.LanguageCode,
                Disabled = false
            }).ToList();

            _dbSet.AddRange(dtos);

            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Guid> entityPropertiesIds)
        {
            var dtos = entityPropertiesIds.Select(id => new EntityPropertyTranslationDTO()
            {
                Id = id
            });
            _dbSet.RemoveRange(dtos);

            return Task.CompletedTask;
        }
    }
}