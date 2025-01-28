export class UserOrganisationDetails {
  id: string;
  permissionIds: string[];

  constructor(payload: UserOrganisationDetailsDTO) {
    this.id = payload.id;
    this.permissionIds = payload.permissionIds;
  }
}

export interface UserOrganisationDetailsDTO {
  id: string;
  permissionIds: string[];
}