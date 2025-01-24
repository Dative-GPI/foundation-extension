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
    public class RolePermissionOrganisationRepository : IRolePermissionOrganisationRepository
    {
        private readonly DbSet<RolePermissionOrganisationDTO> _dbSet;

        public RolePermissionOrganisationRepository(BaseApplicationContext context)
        {
            _dbSet = context.RolePermissionOrganisations;
        }

        public async Task<BasePermissionDetails> Get(Guid id)
        {
            var permissions = await _dbSet
                .Include(p => p.PermissionOrganisation)
                .Where(p => p.RoleId == id)
                .AsNoTracking()
                .ToListAsync();

            return new BasePermissionDetails()
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