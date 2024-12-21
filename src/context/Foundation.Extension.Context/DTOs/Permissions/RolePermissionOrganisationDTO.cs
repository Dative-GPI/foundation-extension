using System;

using Bones.Repository.Interfaces;

namespace Foundation.Extension.Context.DTOs
{
    public class RolePermissionOrganisationDTO : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionOrganisationId { get; set; }
        public PermissionOrganisationDTO PermissionOrganisation { get; set; }
        public bool Disabled { get; set; }
    }
}