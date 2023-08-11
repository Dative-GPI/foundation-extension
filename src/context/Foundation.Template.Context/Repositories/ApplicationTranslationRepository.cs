using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Foundation.Template.Context.DTOs;

using Foundation.Template.Domain.Models;
using Foundation.Template.Domain.Repositories.Commands;
using Foundation.Template.Domain.Repositories.Interfaces;
using Foundation.Template.Domain.Repositories.Filters;

namespace Foundation.Template.Context.Repositories
{
    public class ApplicationTranslationRepository : IApplicationTranslationRepository
    {
        private DbSet<ApplicationTranslationDTO> _dbSet;

        public ApplicationTranslationRepository(ApplicationContext context)
        {
            _dbSet = context.ApplicationTranslations;
        }

        public async Task<ApplicationTranslation> Get(Guid id)
        {
            var dto = await _dbSet
                .Include(a => a.Translation)
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (dto == default)
            {
                return null;
            }

            return new ApplicationTranslation()
            {
                Id = dto.Id,
                LanguageCode = dto.LanguageCode,
                TranslationCode = dto.Translation.Code,
                Value = dto.Value
            };
        }

        public async Task<IEnumerable<ApplicationTranslation>> GetMany(ApplicationTranslationFilter filter)
        {
            IQueryable<ApplicationTranslationDTO> query = _dbSet
                .Include(ta => ta.Translation)
                .Where(ta => ta.ApplicationId == filter.ApplicationId);

            if (!string.IsNullOrEmpty(filter.LanguageCode))
            {
                query = query.Where(ta => ta.LanguageCode == filter.LanguageCode);
            }
            if (filter.Codes != null)
            {
                query = query.Where(ta => filter.Codes.Contains(ta.Translation.Code));
            }

            var dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(translationDTO => new ApplicationTranslation()
            {
                Id = translationDTO.Id,
                LanguageCode = translationDTO.LanguageCode,
                TranslationCode = translationDTO.Translation.Code,
                Value = translationDTO.Value
            }).ToList();
        }

        public Task CreateRange(IEnumerable<CreateApplicationTranslation> payload)
        {
            _dbSet.AddRange(payload.Select(t => new ApplicationTranslationDTO()
            {
                ApplicationId = t.ApplicationId,
                Id = Guid.NewGuid(),
                LanguageCode = t.LanguageCode,
                TranslationId = t.TranslationId,
                Value = t.Value,
                Disabled = false
            }));

            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Guid> translationIds)
        {
            var dtos = translationIds.Select(t => new ApplicationTranslationDTO()
            {
                Id = t
            });

            _dbSet.RemoveRange(dtos);

            return Task.CompletedTask;
        }
    }
}