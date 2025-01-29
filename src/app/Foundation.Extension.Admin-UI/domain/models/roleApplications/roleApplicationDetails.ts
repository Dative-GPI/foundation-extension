export class RoleApplicationDetails {
  id: string;
  permissionIds: string[];

  constructor(params: RoleApplicationDetailsDTO) {
    this.id = params.id;
    this.permissionIds = params.permissionIds;
  }
}

export interface RoleApplicationDetailsDTO {
  id: string;
  permissionIds: string[];
}

export interface UpdateRoleApplicationDTO {
  permissionIds: string[];
}