export class RoleOrganisationTypeDetails {
  id: string;
  permissionIds: string[];

  constructor(params: RoleOrganisationTypeDetailsDTO) {
    this.id = params.id;
    this.permissionIds = params.permissionIds;
  }
}

export interface RoleOrganisationTypeDetailsDTO {
  id: string;
  permissionIds: string[];
}

export interface UpdateRoleOrganisationTypeDTO {
  permissionIds: string[];
}
