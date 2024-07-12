using System;
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
    public class PermissionApplicationRepository : IPermissionApplicationRepository
    {
        private DbSet<PermissionApplicationDTO> _dbSet;

        public PermissionApplicationRepository(BaseApplicationContext context)
        {
            _dbSet = context.PermissionApplications;
        }

        public async Task<IEnumerable<PermissionApplicationInfos>> GetMany(PermissionApplicationFilter filter)
        {
            var query = _dbSet
                .AsQueryable();

            if (filter.PermissionApplicationIds != null)
            {
                query = query.Where(p => filter.PermissionApplicationIds.Contains(p.Id));
            }

            if (!String.IsNullOrWhiteSpace(filter.Search))
            {
                string caseInsensitiveSearch = filter.Search.ToLowerInvariant();
                query = query.Where(p => 
                    p.LabelDefault.ToLowerInvariant().Contains(caseInsensitiveSearch) ||
                    p.Code.ToLowerInvariant().Contains(caseInsensitiveSearch)    
                );
            }

            IEnumerable<PermissionApplicationDTO> dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(permissionDTO => new PermissionApplicationInfos()
            {
                Id = permissionDTO.Id,
                Code = permissionDTO.Code,
                Label = permissionDTO.LabelDefault,
                Translations = permissionDTO.Translations?.Select(t => new TranslationPermissionApplication()
                {
                    LanguageCode = t.LanguageCode,
                    Label = t.Label
                })?.ToList() ?? new List<TranslationPermissionApplication>()
            }).ToList();
        }
    }
}