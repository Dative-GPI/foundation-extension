export class RoleApplicationDetails {
    permissionIds: string[];

    constructor(params: RoleApplicationDetailsDTO) {
        this.permissionIds = params.permissionIds;
    }
}

export interface RoleApplicationDetailsDTO {
    permissionIds: string[];
}

export interface UpdateRoleApplicationDTO {
    permissionIds: string[];
}
