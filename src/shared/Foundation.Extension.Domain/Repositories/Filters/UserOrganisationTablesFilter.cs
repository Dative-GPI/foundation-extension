using System;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class UserOrganisationTablesFilter
    {
        public Guid? UserOrganisationId { get; set; }
        public Guid? TableId { get; set; }
    }
}