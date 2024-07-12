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
    public class PermissionOrganisationRepository : IPermissionOrganisationRepository
    {
        private DbSet<PermissionOrganisationDTO> _dbSet;

        public PermissionOrganisationRepository(BaseApplicationContext context)
        {
            _dbSet = context.PermissionOrganisations;
        }

        public async Task<IEnumerable<PermissionOrganisationInfos>> GetMany(PermissionsFilter filter)
        {
            var query = _dbSet
                .AsQueryable();

            if (filter.PermissionIds != null)
            {
                query = query.Where(p => filter.PermissionIds.Contains(p.Id));
            }

            if (!String.IsNullOrWhiteSpace(filter.Search))
            {
                string caseInsensitiveSearch = filter.Search.ToLowerInvariant();
                query = query.Where(p => 
                    p.LabelDefault.ToLowerInvariant().Contains(caseInsensitiveSearch) ||
                    p.Code.ToLowerInvariant().Contains(caseInsensitiveSearch)    
                );
            }

            IEnumerable<PermissionOrganisationDTO> dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(permissionDTO => new PermissionOrganisationInfos()
            {
                Id = permissionDTO.Id,
                Code = permissionDTO.Code,
                Label = permissionDTO.LabelDefault,
                Translations = permissionDTO.Translations?.Select(t => new TranslationPermissionOrganisation()
                {
                    LanguageCode = t.LanguageCode,
                    Label = t.Label
                })?.ToList() ?? new List<TranslationPermissionOrganisation>()
            }).ToList();
        }
    }
}