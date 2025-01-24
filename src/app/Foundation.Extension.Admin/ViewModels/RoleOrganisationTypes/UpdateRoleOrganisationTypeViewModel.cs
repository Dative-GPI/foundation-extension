using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Foundation.Extension.Admin.ViewModels
{
    public class UpdateRoleOrganisationTypeViewModel
    {
        public List<Guid> PermissionIds { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
    }
}