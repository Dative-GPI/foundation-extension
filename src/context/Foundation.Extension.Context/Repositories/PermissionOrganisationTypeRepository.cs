using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Context.DTOs;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Context.Repositories
{
    public class PermissionOrganisationTypeRepository : IPermissionOrganisationTypeRepository
    {
        private DbSet<PermissionOrganisationTypeDTO> _dbSet;

        public PermissionOrganisationTypeRepository(BaseApplicationContext context)
        {
            _dbSet = context.PermissionOrganisationTypes;
        }

        public async Task<IEnumerable<PermissionOrganisationTypeInfos>> GetMany(PermissionOrganisationTypesFilter filter)
        {
            var query = _dbSet.Include(p => p.Permission).AsQueryable();

            if (filter.OrganisationTypeId.HasValue)
            {
                query = query.Where(p => p.OrganisationTypeId == filter.OrganisationTypeId);
            }

            if(filter.OrganisationTypeIds != null)
            {
                query = query.Where(p => filter.OrganisationTypeIds.Contains(p.OrganisationTypeId));
            }

            IEnumerable<PermissionOrganisationTypeDTO> dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(permissionOrganisationTypeDTO => new PermissionOrganisationTypeInfos()
            {
                Id = permissionOrganisationTypeDTO.Id,
                PermissionId = permissionOrganisationTypeDTO.PermissionId,
                PermissionCode = permissionOrganisationTypeDTO.Permission.Code,
                PermissionLabel = permissionOrganisationTypeDTO.Permission.LabelDefault,
                TranslationPermissions = permissionOrganisationTypeDTO.Permission.Translations?.Select(t => new TranslationPermissionOrganisation()
                {
                    LanguageCode = t.LanguageCode,
                    Label = t.Label
                }).ToList() ?? new List<TranslationPermissionOrganisation>(),
                OrganisationTypeId = permissionOrganisationTypeDTO.OrganisationTypeId,
            }).ToList();
        }

        public Task RemoveRange(Guid[] permissionsIds)
        {
            _dbSet.RemoveRange(permissionsIds.Select(id => new PermissionOrganisationTypeDTO()
            {
                Id = id
            }));

            return Task.CompletedTask;
        }

        public Task CreateRange(IEnumerable<CreatePermissionOrganisationType> payload)
        {
            _dbSet.AddRange(payload.Select(p => new PermissionOrganisationTypeDTO()
            {
                Id = Guid.NewGuid(),
                OrganisationTypeId = p.OrganisationTypeId,
                PermissionId = p.PermissionId,
                Disabled = false
            }));

            return Task.CompletedTask;
        }
    }
}