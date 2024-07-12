using System;

namespace Foundation.Extension.Domain.Models
{
    public class UserOrganisationTable
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public Guid UserOrganisationId { get; set; }
        public string Mode { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int RowsPerPage { get; set; }
        public bool Disabled { get; set; }
    }
}