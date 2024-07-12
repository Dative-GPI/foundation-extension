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
            var result = await _dbSet.ToListAsync();
            return result.Select(r => new Translation()
            {
                Id = r.Id,
                Code = r.Code,
                Value = r.ValueDefault
            });
        }
    }
}