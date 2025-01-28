using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Bones.Domain;
using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Context.DTOs;

namespace Foundation.Extension.Context.Repositories
{
    public class RoleOrganisationTypeRepository : IRoleOrganisationTypeRepository
    {
        private readonly DbSet<RolePermissionOrganisationDTO> _dbSet;

        public RoleOrganisationTypeRepository(BaseApplicationContext context)
        {
            _dbSet = context.RolePermissionOrganisations;
        }

        public async Task<RoleOrganisationTypeDetails> Get(Guid roleOrganisationTypeId)
        {
            var permissions = await _dbSet
                .Include(p => p.PermissionOrganisation)
                .Where(p => p.RoleId == roleOrganisationTypeId)
                .AsNoTracking()
                .ToListAsync();

            return new RoleOrganisationTypeDetails()
            {
                Id = roleOrganisationTypeId,
                Permissions = permissions.Select(p => new PermissionItem()
                {
                    Id = p.PermissionOrganisationId,
                    Code = p.PermissionOrganisation.Code
                }).ToList()
            };
        }

        public async Task<IEntity<Guid>> Update(UpdateRoleOrganisationType payload)
        {
            var formerPermissions = await _dbSet.Where(p => p.RoleId == payload.Id).ToListAsync();

            _dbSet.RemoveRange(formerPermissions);

            _dbSet.AddRange(payload.PermissionIds.Select(p => new RolePermissionOrganisationDTO()
            {
                Id = Guid.NewGuid(),
                RoleId = payload.Id,
                PermissionOrganisationId = p
            }));

            return new FakeEntity<Guid>(payload.Id);
        }
    }
}