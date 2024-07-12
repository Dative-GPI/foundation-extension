using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Context.DTOs;
using Bones.Domain;

namespace Foundation.Extension.Context.Repositories
{
    public class RolePermissionOrganisationRepository : IRolePermissionOrganisationRepository
    {
        private DbSet<RolePermissionOrganisationDTO> _dbSet;

        public RolePermissionOrganisationRepository(BaseApplicationContext context)
        {
            _dbSet = context.RolePermissionOrganisations;
        }

        public async Task<RolePermissionOrganisationDetails> Get(Guid id)
        {
            var permissions = await _dbSet
                .Include(p => p.PermissionOrganisation)
                .Where(p => p.RoleOrganisationId == id)
                .AsNoTracking()
                .ToListAsync();

            return new RolePermissionOrganisationDetails()
            {
                Id = id,
                Permissions = permissions.Select(p => new PermissionItem()
                {
                    Id = p.PermissionOrganisationId,
                    Code = p.PermissionOrganisation.Code
                }).ToList()
            };
        }

        public async Task<IEntity<Guid>> Update(UpdateRolePermissionOrganisation payload)
        {
            var formerPermissions = await _dbSet.Where(p => p.RoleOrganisationId == payload.Id).ToListAsync();

            _dbSet.RemoveRange(formerPermissions);

            _dbSet.AddRange(payload.PermissionIds.Select(p => new RolePermissionOrganisationDTO()
            {
                Id = Guid.NewGuid(),
                RoleOrganisationId = payload.Id,
                PermissionOrganisationId = p
            }));

            return new FakeEntity<Guid>(payload.Id);
        }
    }
}