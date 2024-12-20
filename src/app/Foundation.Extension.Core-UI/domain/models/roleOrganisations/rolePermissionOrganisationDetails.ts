export class RolePermissionOrganisationDetails {
    id: string;
    permissionIds: string[];

    constructor(payload: RolePermissionOrganisationDetailsDTO) {
        this.id = payload.id;
        this.permissionIds = payload.permissionIds;
    }
}

export interface RolePermissionOrganisationDetailsDTO {
    id: string;
    permissionIds: string[];
}

export interface UpdateRolePermissionOrganisationDTO {
    permissionIds: string[];
}