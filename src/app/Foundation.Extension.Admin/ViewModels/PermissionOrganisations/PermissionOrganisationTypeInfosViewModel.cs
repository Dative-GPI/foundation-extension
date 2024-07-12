using System;

namespace Foundation.Extension.Admin.ViewModels
{
    public class PermissionOrganisationTypeInfosViewModel
    {
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public Guid OrganisationTypeId { get; set; }

        #region translation
        public string PermissionLabel { get; set; }
        #endregion
    }
}