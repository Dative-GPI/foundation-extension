export class UserPermissionOrganisationDetails {
    id: string;
    permissionIds: string[];

    constructor(payload: UserPermissionOrganisationDetailsDTO) {
        this.id = payload.id;
        this.permissionIds = payload.permissionIds;
    }
}

export interface UserPermissionOrganisationDetailsDTO {
    id: string;
    permissionIds: string[];
}