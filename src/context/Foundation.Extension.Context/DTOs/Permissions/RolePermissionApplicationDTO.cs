using System;

using Bones.Repository.Interfaces;

namespace Foundation.Extension.Context.DTOs
{
    public class RolePermissionApplicationDTO : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid RoleApplicationId { get; set; }
        public Guid PermissionApplicationId { get; set; }
        public PermissionApplicationDTO PermissionApplication { get; set; }
        public bool Disabled { get; set; }
    }
}