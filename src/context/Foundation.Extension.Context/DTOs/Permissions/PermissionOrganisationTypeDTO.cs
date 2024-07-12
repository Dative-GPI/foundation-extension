using System;

using Bones.Repository.Interfaces;

namespace Foundation.Extension.Context.DTOs
{
    public class PermissionOrganisationTypeDTO : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid OrganisationTypeId { get; set; }
        public Guid PermissionId { get; set; }
        public PermissionOrganisationDTO Permission { get; set; }
        public bool Disabled { get; set; }
    }
}