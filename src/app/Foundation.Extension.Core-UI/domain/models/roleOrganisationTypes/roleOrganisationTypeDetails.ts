export class RoleOrganisationTypeDetails {
  id: string;
  permissionIds: string[];

  constructor(payload: RoleOrganisationTypeDetailsDTO) {
    this.id = payload.id;
    this.permissionIds = payload.permissionIds;
  }
}

export interface RoleOrganisationTypeDetailsDTO {
  id: string;
  permissionIds: string[];
}