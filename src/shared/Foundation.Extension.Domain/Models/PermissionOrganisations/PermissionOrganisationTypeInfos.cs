using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public class PermissionOrganisationTypeInfos
    {
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public Guid OrganisationTypeId { get; set; }
        public string PermissionLabel { get; set; }
        public List<TranslationPermissionOrganisation> TranslationPermissions { get; set; }
    }
}