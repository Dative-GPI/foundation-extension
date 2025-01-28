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
    public class RoleApplicationRepository : IRoleApplicationRepository
    {
        private readonly DbSet<RolePermissionApplicationDTO> _dbSet;

        public RoleApplicationRepository(BaseApplicationContext context)
        {
            _dbSet = context.RolePermissionApplications;
        }

        public async Task<RoleApplicationDetails> Get(Guid id)
        {
            var permissions = await _dbSet
                .Include(p => p.PermissionApplication)
                .Where(p => p.RoleApplicationId == id)
                .AsNoTracking()
                .ToListAsync();

            return new RoleApplicationDetails()
            {
                Id = id,
                Permissions = permissions.Select(p => new PermissionItem()
                {
                    Id = p.PermissionApplicationId,
                    Code = p.PermissionApplication.Code
                }).ToList()
            };
        }

        public async Task<IEntity<Guid>> Update(UpdateRoleApplication payload)
        {
            var formerPermissions = await _dbSet.Where(p => p.RoleApplicationId == payload.Id).ToListAsync();

            _dbSet.RemoveRange(formerPermissions);

            _dbSet.AddRange(payload.PermissionIds.Select(p => new RolePermissionApplicationDTO()
            {
                Id = Guid.NewGuid(),
                RoleApplicationId = payload.Id,
                PermissionApplicationId = p
            }));

            return new FakeEntity<Guid>(payload.Id);
        }
    }
}