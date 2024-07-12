export class RolePermissionOrganisationDetails {
    permissionIds: string[];

    constructor(params: RolePermissionOrganisationDetailsDTO) {
        this.permissionIds = params.permissionIds;
    }
}

export interface RolePermissionOrganisationDetailsDTO {
    permissionIds: string[];
}

export interface UpdateRolePermissionOrganisation {
    permissionIds: string[];
}
