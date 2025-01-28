using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Context.DTOs;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Context.Repositories
{
    public class PermissionApplicationCategoryRepository : IPermissionApplicationCategoryRepository
    {
        private DbSet<PermissionApplicationCategoryDTO> _categorySet;

        public PermissionApplicationCategoryRepository(BaseApplicationContext context)
        {
            _categorySet = context.PermissionApplicationCategories;
        }

        public async Task<IEnumerable<PermissionApplicationCategoryInfos>> GetMany()
        {
            var dtos = await _categorySet.AsNoTracking().ToListAsync();

            return dtos.Select(c => new PermissionApplicationCategoryInfos()
            {
                Label = c.LabelDefault,
                Prefix = c.Prefix,
                Translations = c.Translations?.Select(t => new TranslationPermissionApplicationCategory()
                {
                    Label = t.Label,
                    LanguageCode = t.LanguageCode
                }).ToList() ?? new List<TranslationPermissionApplicationCategory>()
            }).ToList();
        }
    }
}