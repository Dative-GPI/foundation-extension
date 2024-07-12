using System;

namespace Foundation.Extension.Core.Models
{
    public class RequestContext
    {
        public Guid ApplicationId { get; set; }
        public Guid ActorId { get; set; }

        public Guid? OrganisationId { get; set; }
        public Guid? OrganisationTypeId { get; set; }
        public string LanguageCode { get; set; }
        public string Jwt { get; set; }
        public Guid? OrganisationAdminId { get; set; }
        public Guid? ActorOrganisationId { get; set; }
        public Guid? RoleId { get; set; }
    }
}