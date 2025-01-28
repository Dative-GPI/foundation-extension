export class RoleOrganisationDetails {
  id: string;
  permissionIds: string[];

  constructor(payload: RoleOrganisationDetailsDTO) {
    this.id = payload.id;
    this.permissionIds = payload.permissionIds;
  }
}

export interface RoleOrganisationDetailsDTO {
  id: string;
  permissionIds: string[];
}

export interface UpdateRoleOrganisationDTO {
  permissionIds: string[];
}