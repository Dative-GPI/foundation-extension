using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Context.DTOs;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Context.Repositories
{
    public class TranslationRepository : ITranslationRepository
    {
        private BaseApplicationContext _context;
        private DbSet<TranslationDTO> _dbSet;

        public TranslationRepository(BaseApplicationContext context)
        {
            _context = context;
            _dbSet = context.Translations;
        }

        public async Task<IEnumerable<Translation>> GetMany()
        {
            var query = _dbSet
                .AsQueryable();

            var dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(t => new Translation()
            {
                Id = t.Id,
                Code = t.Code,
                Value = t.ValueDefault,
                Translations = t.Translations?.Select(t => new TranslationTranslation()
                {
                    LanguageCode = t.LanguageCode,
                    Value = t.Value
                }).ToList() ?? new List<TranslationTranslation>()
            }).OrderBy(t => t.Code).ToList();
        }
    }
}